using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Common.Dtos.Profile;
using Common.Dtos.Profile.Validators;
using FluentValidation;
using Common.Dtos.Profile;
using Common.Dtos.Profile.Validators;
using FluentValidation;
using Frontend.Components;
using Frontend.Helpers;
using Frontend.Services;
using Frontend.Services.Contracts;
using Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
        options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
        options.AccessDeniedPath = "/access-denied";
    });

builder.Services.AddAntiforgery(options =>
    {
        options.Cookie.Expiration = TimeSpan.Zero;
    });
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(EnvFileHelper.GetString("API_URL")) });
builder.Services.AddScoped<ApiHelper>();

// Profile features
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddTransient<IValidator<ProfileDto>, ProfileDtoValidator>();
builder.Services.AddTransient<IValidator<ExperienceDto>, ExperienceDtoValidator>();
builder.Services.AddTransient<IValidator<EducationDto>, EducationDtoValidator>();
builder.Services.AddTransient<IValidator<SkillDto>, SkillDtoValidator>();

builder.Services.AddScoped<IJobService, JobService>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IExternalJobService, ExternalJobService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IUserService, UserService>();

//Interview Services
builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<IInterviewerService, InterviewerService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();

//Position Services
builder.Services.AddScoped<IPositionService, PositionService>();


//Dashboard
builder.Services.AddScoped<IDashboardService, DashboardService>();

//FAQ Services
builder.Services.AddScoped<IFAQService, FAQService>();


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
app.UseRouting();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .DisableAntiforgery()
    .AddInteractiveServerRenderMode();

app.Run();