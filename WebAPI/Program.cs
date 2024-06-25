using ESOF.WebApp.DBLayer.Context;
using Helpers.Models;
using ESOF.WebApp.WebAPI.Repositories;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using ESOF.WebApp.WebAPI.Services;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;
using WebAPI.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<EmailTemplateService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ISkillRepository, SkillRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository >();
builder.Services.AddScoped<IExperienceRepository, ExperienceRepository>();
builder.Services.AddScoped<RoleRepository>();
builder.Services.AddScoped<RegisterRepository>();

builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IJobSkillRepository, JobSkillRepository>();


//Interview Repository
builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
builder.Services.AddScoped<IInterviewerRepository, InterviewerRepository>();
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
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

app.MapGet("/users/emails", () =>
    {
        var db = new ApplicationDbContext();
        return db.Users.Select(u => u.Email);
    })
    .WithName("GetUsersNames")
    .WithOpenApi();



app.MapGet("/users/with_permissions", () =>
    {
        var db = new ApplicationDbContext();
        return db.Users.Select(u => new UserWithPermissionsModel()
        {
            Email = u.Email,
            UserId = u.UserId,
            Permissions = u.UserRoles
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => new PermissionModel()
                {
                    Name = rp.Permission.Name
                })
        });
    })
    .WithName("GetUsersWithPermissions")
    .WithOpenApi();
app.MapControllers();


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}