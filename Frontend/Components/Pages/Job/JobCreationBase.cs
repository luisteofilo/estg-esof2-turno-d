using Common.Dtos.Job;
using Common.Dtos.Profile;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages.Job;

public class JobCreationBase : ComponentBase
{
    [Inject] IProfileService ProfileService { get; set; }
    [Inject] IJobService JobService { get; set; }
    [Inject] protected NavigationManager Navigation { get; set; }

    protected string? ErrorMessage { get; set; }

    protected IEnumerable<SkillDto> AvailableSkills { get; set; } = new List<SkillDto>();
    private List<SkillDto> RequiredSkills { get; set; } = [];
    private List<SkillDto> NiceToHaveSkills { get; set; } = [];

    protected JobDto JobNew { get; set; } = new JobDto();

    protected bool showModal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            AvailableSkills = await ProfileService.GetSkills();
        }
        catch (Exception e)
        {
            ErrorMessage += "Error getting skills";
            Console.WriteLine(e.Message);
        }
    }

    protected void GoBack()
    {
        Navigation.NavigateTo("/jobs");
    }

    protected async Task CreateJob()
    {
        try
        {
            //Hard Coded ClientId
            var JobDto = await JobService.CreateJob(Guid.Parse("392fd8cc-e617-49d0-a2ac-885ee2f0153a"), JobNew);
            await createSkillsForJob(JobDto.JobId);
            showModal = true;
            StateHasChanged();
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage += "Error creating job";
            Console.WriteLine($"Error creating job: {ex.Message}");
        }
    }

    protected void CloseModal()
    {
        showModal = false;
        ClearForm();
    }

    private void ClearForm()
    {
        JobNew = new JobDto();
        StateHasChanged();
    }


    protected void UpdateSkills(ChangeEventArgs e, SkillDto skill, bool isRequired)
    {
        var isChecked = (bool)e.Value;

        if (isRequired)
        {
            if (isChecked && !RequiredSkills.Contains(skill))
            {
                RequiredSkills.Add(skill);
            }
            else if (!isChecked && RequiredSkills.Contains(skill))
            {
                RequiredSkills.Remove(skill);
            }
        }
        else
        {
            if (isChecked && !NiceToHaveSkills.Contains(skill))
            {
                NiceToHaveSkills.Add(skill);
            }
            else if (!isChecked && NiceToHaveSkills.Contains(skill))
            {
                NiceToHaveSkills.Remove(skill);
            }
        }
        StateHasChanged();
    }

    // TODO : Implementing
    private Task createSkillsForJob(Guid jobId)
    {
        try
        {
            foreach (SkillDto RSkill in RequiredSkills)
            {
                var SkillDto = JobService.CreateJobSkill(jobId, RSkill.SkillId, true, new JobSkillDto());
            }
            foreach (SkillDto NtHSkill in NiceToHaveSkills)
            {
                var SkillDto = JobService.CreateJobSkill(jobId, NtHSkill.SkillId, false, new JobSkillDto());
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return Task.CompletedTask;
    }
}