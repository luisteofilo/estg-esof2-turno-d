using Common.Dtos.Job;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages.Job;

public class JobBase : ComponentBase
{
    [Inject] protected IJobService JobService { get; set; }
    [Inject] protected IExternalJobService ExternalJobService { get; set; }
    [Inject] protected NavigationManager Navigation { get; set; }

    protected IEnumerable<JobDto> Jobs { get; set; }

    protected string UrlInput { get; set; } = string.Empty;
    protected string SuccessMessage { get; set; }
    protected string ErrorMessage { get; set; }

    private Timer? _timer;

    protected override async Task OnInitializedAsync()
    {
        Jobs = await JobService.GetJobs();
    }

    protected void ManualRegister()
    {
        Navigation.NavigateTo("/JobCreation");
    }

    private void ClearMessage(object? state)
    {
        InvokeAsync(() =>
        {
            SuccessMessage = string.Empty;
            ErrorMessage = string.Empty;
            StateHasChanged();
        });
        _timer?.Dispose();
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
        SuccessMessage = "Job Offer Created";
        StateHasChanged();
        _timer = new Timer(ClearMessage, null, 5000, Timeout.Infinite);
    }

    protected void RemoveJob(Guid jobId)
    {

    }

}