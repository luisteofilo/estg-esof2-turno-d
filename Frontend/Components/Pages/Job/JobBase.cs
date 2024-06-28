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
    protected IEnumerable<JobDto> FilteredJobs { get; set; }

    protected string UrlInput { get; set; } = string.Empty;
    protected string SuccessMessage { get; set; }
    protected string ErrorMessage { get; set; }

    protected string searchPosition = string.Empty;
    protected string searchLocalization = string.Empty;
    protected string searchCompany = string.Empty;

    private Timer? _timer;

    protected override async Task OnInitializedAsync()
    {
        Jobs = await JobService.GetJobs();
        FilteredJobs = Jobs;
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
        // Implement the job removal logic
    }

    protected void OnSearchPositionChanged(ChangeEventArgs e)
    {
        searchPosition = e.Value.ToString();
        FilterJobs();
    }

    protected void OnSearchLocalizationChanged(ChangeEventArgs e)
    {
        searchLocalization = e.Value.ToString();
        FilterJobs();
    }

    protected void OnSearchCompanyChanged(ChangeEventArgs e)
    {
        searchCompany = e.Value.ToString();
        FilterJobs();
    }

    protected void FilterJobs()
    {
        FilteredJobs = Jobs.Where(j =>
            (string.IsNullOrEmpty(searchPosition) || j.Position.Contains(searchPosition, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(searchLocalization) || j.Localization.Contains(searchLocalization, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrEmpty(searchCompany) || j.Company != null && j.Company.Contains(searchCompany, StringComparison.OrdinalIgnoreCase))
        ).ToList();
    }
}
