using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Persistence;
using ESOF.WebApp.DBLayer.Persistence.Interfaces;
using ESOF.WebApp.DBLayer.Persistence.Repositories;
using ESOF.WebApp.Scraper;
using ESOF.WebApp.WebAPI.Repositories;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using ESOF.WebApp.WebAPI.Services;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using WebAPI.Repositories;
using WebAPI.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext and Hangfire
var dbContext = new ApplicationDbContext();
var connectionString = dbContext.Database.GetDbConnection().ConnectionString;

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(connectionString));

builder.Services.AddHangfireServer();

// Add Swagger/OpenAPI
builder.Services.AddSwaggerGen();

// Register repositories
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
builder.Services.AddScoped<IInterviewerRepository, InterviewerRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IInterviewFeedback, InterviewFeedbackRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobSkillRepository, JobSkillRepository>();
builder.Services.AddScoped<IImportRepository, ImportRepository>();
builder.Services.AddScoped<IExternalJobRepository, ExternalJobRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<JobFAQRepository>();
builder.Services.AddScoped<EmailTemplateService>();

// Add HttpClient service for making HTTP requests
builder.Services.AddHttpClient();

// Add Scraper service
builder.Services.AddScraperDependencyInjection();

var app = builder.Build();

// Configure Hangfire dashboard
app.UseHangfireDashboard("/hangfire");

// Configure Swagger/OpenAPI
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Serve static files (e.g., profile avatars)
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
    RequestPath = new PathString("/Resources")
});

// Configure exception handling
app.UseExceptionHandler("/error");

// Define weather forecast endpoint
var random = new Random();
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index => new WeatherForecast(
        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        random.Next(-20, 55),
        summaries[random.Next(summaries.Length)]
    )).ToArray();

    return Results.Ok(forecast);
})
.WithName("GetWeatherForecast")
.WithMetadata(new
{
    Summary = "Get weather forecast for the next 5 days",
    Tags = new[] { "weather", "forecast" }
});

// Uncomment if needed: Example of retrieving user emails from the database
/*
app.MapGet("/users/emails", () =>
{
    var db = new ApplicationDbContext();
    return db.Users.Select(u => u.Email).ToList();
})
.WithName("GetUsersEmails")
.WithMetadata(new
{
    Summary = "Get emails of all users",
    Tags = new[] { "users", "emails" }
});
*/

// Map controllers
app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
