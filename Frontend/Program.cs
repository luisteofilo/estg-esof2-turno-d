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

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(EnvFileHelper.GetString("API_URL")) });
builder.Services.AddScoped<ApiHelper>();

builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddScoped<IUserService, UserService>();

//Interview Services
builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<IInterviewerService, InterviewerService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorComponents<App>()
    .DisableAntiforgery()
    .AddInteractiveServerRenderMode();

app.Run();