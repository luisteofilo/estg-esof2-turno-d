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

builder.Services.AddScoped<IProfileService, ProfileService>();



builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IExternalJobService, ExternalJobService>();

//Interview Services
builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<IInterviewerService, InterviewerService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();


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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();