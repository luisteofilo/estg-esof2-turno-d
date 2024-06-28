namespace Common.Dtos.Profile;
using ESOF.WebApp.DBLayer.Entities;

public static class DtoConversion
{
    // Métodos para ProfileDto
    public static IEnumerable<ProfileDto> ProfilesConvertToDto(this IEnumerable<Profile> profiles)
    {
        return profiles.Select(profile => profile.ProfileConvertToDto()).ToList();
    }

    public static ProfileDto ProfileConvertToDto(this Profile profile)
    {
        return new ProfileDto
        {
            ProfileId = profile.ProfileId,
            FirstName = profile.FirstName,
            LastName = profile.LastName,
            Bio = profile.Bio,
            Avatar = profile.Avatar,
            Location = profile.Location,
            UrlProfile = profile.UrlProfile,
            UserId = profile.UserId
        };
    }

    public static Profile DtoConvertToProfile(this ProfileDto profileDto)
    {
        return new Profile
        {
            ProfileId = profileDto.ProfileId,
            FirstName = profileDto.FirstName,
            LastName = profileDto.LastName,
            Bio = profileDto.Bio,
            Avatar = profileDto.Avatar,
            Location = profileDto.Location,
            UrlProfile = profileDto.UrlProfile,
            UserId = profileDto.UserId,
        };
    }

    // Métodos para SkillDto
    public static IEnumerable<SkillDto> SkillsConvertToDto(this IEnumerable<Skill> skills)
    {
        return skills.Select(skill => skill.SkillConvertToDto()).ToList();
    }

    public static SkillDto SkillConvertToDto(this Skill skill)
    {
        return new SkillDto
        {
            SkillId = skill.SkillId,
            Name = skill.Name
        };
    }

    public static Skill DtoConvertToSkill(this SkillDto skillDto)
    {
        return new Skill
        {
            SkillId = skillDto.SkillId,
            Name = skillDto.Name
        };
    }

    // Métodos para EducationDto
    public static IEnumerable<EducationDto> EducationsConvertToDto(this IEnumerable<Education> educations)
    {
        return educations.Select(education => education.EducationConvertToDto()).ToList();
    }

    public static EducationDto EducationConvertToDto(this Education education)
    {
        return new EducationDto
        {
            EducationId = education.EducationId,
            ProfileId = education.ProfileId,
            Name = education.Name,
            SchoolName = education.SchoolName,
            StartDate = education.StartDate,
            EndDate = education.EndDate
        };
    }

    public static Education DtoToEducation(this EducationDto educationDto)
    {
        return new Education
        {
            EducationId = educationDto.EducationId,
            ProfileId = educationDto.ProfileId,
            Name = educationDto.Name,
            SchoolName = educationDto.SchoolName,
            StartDate = DateTime.SpecifyKind(educationDto.StartDate, DateTimeKind.Utc),
            EndDate = DateTime.SpecifyKind(educationDto.EndDate, DateTimeKind.Utc)
        };
    }

    // Métodos para ExperienceDto
    public static IEnumerable<ExperienceDto> ExperiencesConvertToDto(this IEnumerable<Experience> experiences)
    {
        return experiences.Select(experience => experience.ExperienceConvertToDto()).ToList();
    }

    public static ExperienceDto ExperienceConvertToDto(this Experience experience)
    {
        return new ExperienceDto
        {
            ExperienceId = experience.ExperienceId,
            ProfileId = experience.ProfileId,
            Name = experience.Name,
            CompanyName = experience.CompanyName,
            StartDate = experience.StartDate,
            EndDate = experience.EndDate,
            Description = experience.Description
        };
    }

    public static Experience DtoToExperience(this ExperienceDto experienceDto)
    {
        return new Experience
        {
            ExperienceId = experienceDto.ExperienceId,
            ProfileId = experienceDto.ProfileId,
            Name = experienceDto.Name,
            CompanyName = experienceDto.CompanyName,
            StartDate = DateTime.SpecifyKind(experienceDto.StartDate, DateTimeKind.Utc),
            EndDate = DateTime.SpecifyKind(experienceDto.EndDate, DateTimeKind.Utc),
            Description = experienceDto.Description
        };
    }
}