using System.Collections;
using Common.Dtos.Profile;

namespace Frontend.Services.Contracts;

public interface IProfileService
{
    // Profile
    Task<ProfileDto> GetProfile(Guid profileId);
    Task<IEnumerable<ProfileDto>> GetProfiles();
    Task<ProfileDto> UpdateProfile(Guid profileId, ProfileDto updatedProfileDto);
    // Skill 
    Task<IEnumerable<SkillDto>> GetSkills();
    Task<IEnumerable<SkillDto>> GetSkillsForProfile(Guid profileId);
    Task AddSkillForProfile(Guid profileId, Guid skillId);
    Task DeleteSkillForProfile(Guid profileId, Guid skillId);
    // Experience 
    Task<IEnumerable<ExperienceDto>> GetExperiencesForProfile(Guid profileId);
    Task<ExperienceDto> CreateExperience(Guid profileId, ExperienceDto experienceDto);
    Task<ExperienceDto> UpdateExperience(Guid profileId, ExperienceDto updatedExperienceDto);
    Task DeleteExperience(Guid profileId, Guid experienceId);
    // Education 
    Task<IEnumerable<EducationDto>> GetEducationsForProfile(Guid profileId);
    Task<EducationDto> CreateEducation(Guid profileId, EducationDto educationDto);
    Task<EducationDto> UpdateEducation(Guid profileId, EducationDto updatedEducationDto);
    Task DeleteEducation(Guid profileId, Guid educationId);
  
}