using Common.Dtos.Job;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages.Job;

public class JobBase : ComponentBase
{
    [Inject] protected IJobService JobService { get; set; }
    [Inject] protected IExternalJobService ExternalJobService { get; set; }

    protected IEnumerable<JobDto> Jobs { get; set; }

    protected string UrlInput { get; set; } = string.Empty;
    protected string SuccessMessage { get; set; }
    protected string ErrorMessage { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Jobs = await JobService.GetJobs();
    }

    protected void ManualRegister()
    {
        Console.WriteLine("Manual Register");
    }

    protected async Task SubmitURL()
    {
        if (string.IsNullOrWhiteSpace(UrlInput))
        {
            ErrorMessage = "The URL is required.";
            return;
        }

        if (!Uri.IsWellFormedUriString(UrlInput, UriKind.Absolute))
        {
            ErrorMessage = "The URL is not valid!";
            return;
        }

        var result = await ExternalJobService.CreateExternalJob(UrlInput);
    }

    protected void RemoveJob(Guid jobId)
    {

    }

}