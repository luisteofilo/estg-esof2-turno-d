using Common.Dtos.Job;
using Common.Dtos.Profile;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages.Job;

public class JobCreationBase : ComponentBase
{
    [Inject] IProfileService ProfileService { get; set; }
    [Inject] IJobService JobService { get; set; }
<<<<<<< HEAD
    
    [Parameter] public string? ClientId { get; set; }
    
    protected string? ErrorMessage { get; set; }
    
    protected IEnumerable<SkillDto> AvailableSkills { get; set; } = new List<SkillDto>();
    private List<SkillDto> RequiredSkills { get; set; } = [];
    private List<SkillDto> NiceToHaveSkills { get; set; } = [];
    
    protected JobDto JobNew { get; set; } = new JobDto();
    
    protected bool showModal { get; set; }
    
=======
    [Inject] protected NavigationManager Navigation { get; set; }

    protected string? ErrorMessage { get; set; }

    protected IEnumerable<SkillDto> AvailableSkills { get; set; } = new List<SkillDto>();
    private List<SkillDto> RequiredSkills { get; set; } = [];
    private List<SkillDto> NiceToHaveSkills { get; set; } = [];

    protected JobDto JobNew { get; set; } = new JobDto();

    protected bool showModal { get; set; }
    protected bool showWarningModal { get; set; }
    protected string WarningMessage { get; set; } = string.Empty;

>>>>>>> origin/master
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
<<<<<<< HEAD
    
    
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
=======

    protected void GoBack()
    {
        Navigation.NavigateTo("/jobs");
    }

    protected async Task CreateJob()
    {
        if (IsFormValid())
        {
            try
            {
                // Hard Coded ClientId
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
        else
        {
            showWarningModal = true;
>>>>>>> origin/master
        }
    }

    protected void CloseModal()
    {
        showModal = false;
        ClearForm();
    }

<<<<<<< HEAD
=======
    protected void CloseWarningModal()
    {
        showWarningModal = false;
    }

>>>>>>> origin/master
    private void ClearForm()
    {
        JobNew = new JobDto();
        StateHasChanged();
    }
<<<<<<< HEAD
    
    
=======

>>>>>>> origin/master
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

<<<<<<< HEAD
    // TODO : Implementing
=======
    private bool IsFormValid()
    {
        var missingFields = new List<string>();

        if (string.IsNullOrWhiteSpace(JobNew.Position))
            missingFields.Add("Position");
        if (string.IsNullOrWhiteSpace(JobNew.EndDate.ToString()) || JobNew.EndDate < DateTime.Today)
            missingFields.Add("End Date (must be today or later)");
        if (string.IsNullOrWhiteSpace(JobNew.Experience))
            missingFields.Add("Experience");
        if (string.IsNullOrWhiteSpace(JobNew.Commitment.ToString()))
            missingFields.Add("Commitment");
        if (string.IsNullOrWhiteSpace(JobNew.Localization))
            missingFields.Add("Localization");
        if (string.IsNullOrWhiteSpace(JobNew.Remote.ToString()))
            missingFields.Add("Remote");
        if (string.IsNullOrWhiteSpace(JobNew.Company))
            missingFields.Add("Company");
        if (string.IsNullOrWhiteSpace(JobNew.Education.ToString()))
            missingFields.Add("Education");

        if (missingFields.Count > 0)
        {
            WarningMessage = "Please fill in the following fields: " + string.Join(", ", missingFields);
            return false;
        }

        return true;
    }

>>>>>>> origin/master
    private Task createSkillsForJob(Guid jobId)
    {
        try
        {
            foreach (SkillDto RSkill in RequiredSkills)
            {
<<<<<<< HEAD
                var SkillDto = JobService.CreateJobSkill(JobNew.JobId, RSkill.SkillId, true, new JobSkillDto());
            }
            foreach (SkillDto NtHSkill in NiceToHaveSkills)
            {
                var SkillDto = JobService.CreateJobSkill(JobNew.JobId, NtHSkill.SkillId, false, new JobSkillDto());
            }
            
            Console.WriteLine("Metodo para atualizar tabela de muitos para muitos" +
                              "fazer a logica e chamar os servicos de acordo com o endpoint");
=======
                var SkillDto = JobService.CreateJobSkill(jobId, RSkill.SkillId, true, new JobSkillDto());
            }
            foreach (SkillDto NtHSkill in NiceToHaveSkills)
            {
                var SkillDto = JobService.CreateJobSkill(jobId, NtHSkill.SkillId, false, new JobSkillDto());
            }
>>>>>>> origin/master
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return Task.CompletedTask;
    }
}