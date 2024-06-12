
using Common.Dtos.Profile;
using ESOF.WebApp.DBLayer.Entities;

public static class DtoConversion
{
    public static ProfileDto ToDto(this Profile pro)
    {
        return new ProfileDto
        {
                FirstName = pro.FirstName,
                LastName = pro.LastName,
                Bio = pro.Bio,
                Avatar = pro.Avatar,
                Location = pro.Location,
                UrlBackground = pro.UrlBackground,
                UrlProfile = pro.UrlProfile,
                Skills = pro.ProfileSkills.Select(ps => ps.Skill.Name).ToList(),
                Experiences = pro.Experiences.Select(exp => new ExperienceDto
                {
                    Name = exp.Name,
                    CompanyName = exp.CompanyName,
                    Duration = exp.Duration,
                    Description = exp.Description
                }).ToList(),
                Educations = pro.Educations.Select(edu => new EducationDto
                {
                     Name = edu.Name,
                     SchoolName = edu.SchoolName,
                     StartDate = edu.StartDate,
                     EndDate = edu.EndDate
                }).ToList()
        };
    }
}