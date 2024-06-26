using System.Net;
using Common.Dtos.Profile;
using Frontend.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace Frontend.Components.Pages.Profile;

public class ProfileBase : ComponentBase
{
    [Inject] protected IProfileService ProfileService { get; set; }

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
    

    protected override async Task OnInitializedAsync()
    {
        // TODO : HARD CODED PROFILE 
        var profileId = Guid.Parse("e69a1fad-6acd-4566-9de9-e95ac88d118a");
        
        Profile = await ProfileService.GetProfile(profileId);
        Skills = await ProfileService.GetSkillsForProfile(profileId);
        AllSkills = await ProfileService.GetSkills();
        Experiences = await ProfileService.GetExperiencesForProfile(profileId);
        Educations = await ProfileService.GetEducationsForProfile(profileId);
    }
    
    // Profile 
    
    protected async Task SaveProfile()
    {
        IsEditingProfile = false;
        Profile = await ProfileService.UpdateProfile(EditableProfile.ProfileId, EditableProfile);
        Skills = await SaveSkills();
        StateHasChanged();
    }
    
    protected void EditProfile(bool edit)
    {
        IsEditingProfile = edit;
        if (edit)
        {
            EditableProfile = Profile.CopyProfileDto();
            EditableSkills = Skills.ToList();
        }
    }
    
    // Skills 

    private async Task<ICollection<SkillDto>> SaveSkills()
    {
        try
        {
            var skillsToAdd = EditableSkills.Where(es => !Skills.Any(ps => ps.SkillId == es.SkillId));
            var skillsToRemove = Skills.Where(ps => !EditableSkills.Any(es => es.SkillId == ps.SkillId));

            foreach (var skill in skillsToAdd)
            {
                await ProfileService.AddSkillForProfile(Profile.ProfileId, skill.SkillId);
                Skills = Skills.Append(skill);
            }

            foreach (var skill in skillsToRemove)
            {
                await ProfileService.DeleteSkillForProfile(Profile.ProfileId, skill.SkillId);
                Skills = Skills.Where(s => s.SkillId != skill.SkillId); 
            }

            return Skills.ToList(); 
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Error saving skills: {ex.Message}");
            return Skills.ToList();
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
        IsEditingEducations = false;
        
        for (int i = 0; i < EditableEducations.Count; i++)
        {
            var edu = EditableEducations[i];
            try
            {
                if (edu.EducationId == Guid.Empty)
                {
                    var newEdu = await ProfileService.CreateEducation(Profile.ProfileId, edu);
                    EditableEducations[i] = newEdu;
                }
                else
                {
                    var updatedEdu = await ProfileService.UpdateEducation(edu.EducationId, edu);
                    EditableEducations[i] = updatedEdu;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error saving educations record: {ex.Message}");
            }
        }

        Educations = EditableEducations;
        StateHasChanged();
    }
    
    protected void AddEducation()
    {
        var newEducation = new EducationDto();
        EditableEducations.Add(newEducation);
    }
    
    protected void EditEducations(bool edit)
    {
        IsEditingEducations = edit;
        if (edit) EditableEducations = Educations.ToList();
    }
    
    protected void RemoveEducation(EducationDto edu)
    {
        EditableEducations.Remove(edu);
        ProfileService.DeleteEducation(Profile.ProfileId, edu.EducationId);
    }

    // Experiences
    
    protected async Task SaveExperiences()
    {
        IsEditingExperiences = false;
        
        for (int i = 0; i < EditableExperiences.Count; i++)
        {
            var exp = EditableExperiences[i];
            try
            {
                if (exp.ExperienceId == Guid.Empty)
                {
                    var newExp = await ProfileService.CreateExperience(Profile.ProfileId, exp);
                    EditableExperiences[i] = newExp;
                }
                else
                {
                    var updatedExp = await ProfileService.UpdateExperience(exp.ExperienceId, exp);
                    EditableExperiences[i] = updatedExp;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error saving experiences record: {ex.Message}");
            }
        }

        Experiences = EditableExperiences;
        StateHasChanged();
    }
    
    protected void AddExperience()
    {
        var newExperience = new ExperienceDto();
        EditableExperiences.Add(newExperience);
    }
    
    protected void EditExperiences(bool edit)
    {
        IsEditingExperiences = edit;
        if (edit) EditableExperiences = Experiences.ToList();
    }
    
    protected void RemoveExperience(ExperienceDto exp)
    {
        EditableExperiences.Remove(exp);
        ProfileService.DeleteExperience(Profile.ProfileId, exp.ExperienceId);
    }

    
}