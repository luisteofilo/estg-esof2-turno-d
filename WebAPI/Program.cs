using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Helpers;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables
Env.Load();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register ApplicationDbContext with dependency injection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var db = EnvFileHelper.GetString("POSTGRES_DB");
    var user = EnvFileHelper.GetString("POSTGRES_USER");
    var password = EnvFileHelper.GetString("POSTGRES_PASSWORD");
    var port = EnvFileHelper.GetString("POSTGRES_PORT");
    var host = EnvFileHelper.GetString("POSTGRES_HOST");

    if (string.IsNullOrEmpty(db) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password) ||
        string.IsNullOrEmpty(port) || string.IsNullOrEmpty(host))
    {
        throw new InvalidOperationException(
            "Database connection information not fully specified in environment variables.");
    }

    var connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={password}";
    options.UseNpgsql(connectionString);
});

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

app.MapGet("/users/emails", (ApplicationDbContext db) =>
{
    return db.Users.Select(u => u.Email).ToList();
})
.WithName("GetUsersNames")
.WithOpenApi();

// New endpoint to get all companies
app.MapGet("/companies", async (ApplicationDbContext dbContext) =>
{
    return await dbContext.Companies.ToListAsync();
})
.WithName("GetCompanies")
.WithOpenApi();

// New endpoint to add a company
app.MapPost("/companies", async (ApplicationDbContext dbContext, Companies company) =>
{
    dbContext.Companies.Add(company);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/companies/{company.CompanieId}", company);
})
.WithName("CreateCompany")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
