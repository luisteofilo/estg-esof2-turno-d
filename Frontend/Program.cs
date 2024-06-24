using Common.Dtos.Profile;
using Common.Dtos.Profile.Validators;
using FluentValidation;
using Frontend.Components;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Contracts;
using Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(EnvFileHelper.GetString("API_URL")) });
builder.Services.AddScoped<ApiHelper>();

// Profile features
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddTransient<IValidator<ProfileDto>, ProfileDtoValidator>();
builder.Services.AddTransient<IValidator<ExperienceDto>, ExperienceDtoValidator>();
builder.Services.AddTransient<IValidator<EducationDto>, EducationDtoValidator>();
builder.Services.AddTransient<IValidator<SkillDto>, SkillDtoValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();