using Common.Dtos.Profile;
using Frontend.Services.Contracts;
using Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Frontend.Components.Pages.Profile;

public class ProfileBase : ComponentBase
{
    [Inject] private IProfileService ProfileService { get; set; }
    [Parameter] public string? ProfileId { get; set; }
    [Parameter] public string? ProfileUrl { get; set; }

    protected readonly string ApiUrl = EnvFileHelper.GetString("API_URL");

    protected string? ErrorMessage { get; set; }

    protected bool IsEditingProfile { get; set; }
    protected bool IsEditingExperiences { get; set; }
    protected bool IsEditingEducations { get; set; }

    protected ProfileDto Profile { get; set; }
    protected IEnumerable<SkillDto> AllSkills { get; set; }
    protected IEnumerable<SkillDto> Skills { get; set; }
    protected IEnumerable<ExperienceDto> Experiences { get; set; }
    protected IEnumerable<EducationDto> Educations { get; set; }

    protected ProfileDto EditableProfile { get; set; }
    protected List<SkillDto> EditableSkills { get; set; }
    protected List<ExperienceDto> EditableExperiences { get; set; }
    protected List<EducationDto> EditableEducations { get; set; }
    
    protected ExperienceDto? NewExperience { get; set; }
    protected EducationDto? NewEducation { get; set; }
    private IBrowserFile selectedFile { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (!string.IsNullOrEmpty(ProfileId))
            {
                Profile = await ProfileService.GetProfile(Guid.Parse(ProfileId));
            }
            else
            {
                Profile = await ProfileService.GetProfileByUrl(ProfileUrl);
            }
            
            
            Skills = await ProfileService.GetSkillsForProfile(Profile.ProfileId);
            Experiences = await ProfileService.GetExperiencesForProfile(Profile.ProfileId);
            Educations = await ProfileService.GetEducationsForProfile(Profile.ProfileId);
            AllSkills = await ProfileService.GetSkills();
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error retrieving profile ";
            Console.WriteLine($"Fail to retrieve profile: {ex.Message}");
        }
    }

    // Profile 

    protected async Task SaveProfile()
    {
        try
        {
            await SaveAvatar();
            await SaveSkills();
            Profile = await ProfileService.UpdateProfile(Profile.ProfileId, EditableProfile);
            
            IsEditingProfile = false;
            StateHasChanged();
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error saving profile ";
            Console.WriteLine($"Error saving profile: {ex.Message}");
        }
        
    }

    protected void EditProfile(bool edit)
    {
        IsEditingProfile = edit;
        if (!edit) return;
        EditableProfile = Profile.Clone();
        EditableSkills = Skills.Select(skill => skill.Clone()).ToList();
    }

    protected void AvatarSelected(InputFileChangeEventArgs e)
    {
        selectedFile = e.File;
    }

    private async Task SaveAvatar()
    {
        try
        {
            if (selectedFile != null)
            {
                EditableProfile.Avatar =  await ProfileService.UploadProfileImageAsync(Profile.ProfileId, selectedFile);
            }
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error uploading profile avatar ";
            Console.WriteLine($"Error uploading profile avatar: {ex.Message}");
        }
    }
    

    // Skills 

    private async Task SaveSkills()
    {
        try
        {
            var skillsToAdd = EditableSkills.Where(es => !Skills.Any(ps => ps.SkillId == es.SkillId));
            var skillsToRemove = Skills.Where(ps => !EditableSkills.Any(es => es.SkillId == ps.SkillId));

            foreach (var skill in skillsToAdd)
            {
                await ProfileService.AddSkillForProfile(Profile.ProfileId, skill.SkillId);
            }

            foreach (var skill in skillsToRemove)
            {
                await ProfileService.DeleteSkillForProfile(Profile.ProfileId, skill.SkillId);
                EditableSkills = Skills.Where(s => s.SkillId != skill.SkillId).ToList();
            }

            Skills = EditableSkills.Select(skill => skill.Clone()).ToList();
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error saving skills ";
            Console.WriteLine($"Error saving skills: {ex.Message}");
        }
    }

    protected void AddSkill(SkillDto skill)
    {
        EditableSkills.Add(skill);
    }

    protected void RemoveSkill(SkillDto skill)
    {
        EditableSkills.Remove(skill);
    }

    // Educations 

    protected async Task SaveEducations()
    {
        try
        {
            for (int i = 0; i < EditableEducations.Count; i++)
            {
                var edu = EditableEducations[i];
                var updatedEdu = await ProfileService.UpdateEducation(Profile.ProfileId, edu);
                EditableEducations[i] = updatedEdu;
            }
            Educations = EditableEducations;
            IsEditingEducations = false;
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error saving educations ";
            Console.WriteLine($"Error saving educations: {ex.Message}");
        }
    }
    
    protected void AddEducation(bool newEdu)
    {
        NewEducation = newEdu ? new EducationDto() : null;
        StateHasChanged();
    }

    protected async Task SaveNewEducation()
    {
        try
        {
            var newEdu = await ProfileService.CreateEducation(Profile.ProfileId, NewEducation!);
            EditableEducations.Add(newEdu);
            AddEducation(false);
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error saving new education ";
            Console.WriteLine($"Error saving new education: {ex.Message}");
        }
    }
    
    protected void EditEducations(bool edit)
    {
        IsEditingEducations = edit;
        if (edit) EditableEducations = Educations.Select(exp => exp.Clone()).ToList();
    }
    
    protected void RemoveEducation(EducationDto edu)
    {
        try
        {
            EditableEducations.Remove(edu);
            ProfileService.DeleteEducation(Profile.ProfileId, edu.EducationId);
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error deleting education ";
            Console.WriteLine($"Error deleting education: {ex.Message}");
        }
        
    }

    // Experiences

    protected async Task SaveExperiences()
    {
        try
        {
            for (int i = 0; i < EditableExperiences.Count; i++)
            {
                var exp = EditableExperiences[i];
                var updatedExp = await ProfileService.UpdateExperience(Profile.ProfileId, exp);
                EditableExperiences[i] = updatedExp;
            }
            Experiences = EditableExperiences;
            IsEditingExperiences = false;
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error saving experiences ";
            Console.WriteLine($"Error saving experiences: {ex.Message}");
        }
    }
    
    protected void AddExperience(bool newExp)
    {
        NewExperience = newExp ? new ExperienceDto() : null;
        StateHasChanged();
    }

    protected async Task SaveNewExperience()
    {
        try
        {
            var newExp = await ProfileService.CreateExperience(Profile.ProfileId, NewExperience!);
            EditableExperiences.Add(newExp);
            AddExperience(false);
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error saving new experience ";
            Console.WriteLine($"Error saving new experience: {ex.Message}");
        }
    }
    
    protected void EditExperiences(bool edit)
    {
        IsEditingExperiences = edit;
        if (edit) EditableExperiences = Experiences.Select(exp => exp.Clone()).ToList();
    }
    
    protected void RemoveExperience(ExperienceDto exp)
    {
        try
        {
            EditableExperiences.Remove(exp);
            ProfileService.DeleteExperience(Profile.ProfileId, exp.ExperienceId);
        }
        catch (Exception ex)
        {
            ErrorMessage += "Error deleting experience ";
            Console.WriteLine($"Error deleting experience: {ex.Message}");
        }
        
    }
}