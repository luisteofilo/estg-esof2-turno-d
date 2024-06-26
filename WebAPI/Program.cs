using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.WebAPI.Repositories;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using ESOF.WebApp.WebAPI.Services; // Add this namespace for EmailTemplateService
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories; // Adjust namespaces accordingly
using WebAPI.Repositories.Contracts; // Adjust namespaces accordingly

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Register repositories with their respective interfaces
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
builder.Services.AddScoped<IInterviewerRepository, InterviewerRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<IInterviewFeedback, InterviewFeedbackRepository>(); // Assuming this is correct
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobSkillRepository, JobSkillRepository>();

// Register services
builder.Services.AddScoped<EmailTemplateService>(); // Assuming EmailTemplateService is correctly implemented and registered here

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Define endpoints
app.MapGet("/weatherforecast", () =>
{
    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

// Assuming this endpoint was intended, uncomment if needed
/*
app.MapGet("/users/emails", () =>
    {
        var db = new ApplicationDbContext();
        return db.Users.Select(u => u.Email);
    })
    .WithName("GetUsersNames")
    .WithOpenApi();
*/

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
