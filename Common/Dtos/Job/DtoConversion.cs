namespace Common.Dtos.Job;
using ESOF.WebApp.DBLayer.Entities;
using Common.Dtos.Profile;

public static class DtoConversion
{
    public static IEnumerable<JobDto> JobsConvertToDto(this IEnumerable<Job> jobs)
    {
        return jobs.Select(job => job.JobConvertToDto()).ToList();
    }
    
    public static JobDto JobConvertToDto(this Job job)
    {
        return new JobDto
        {
            JobId = job.JobId,
            ClientId = job.ClientId,
            EndDate = job.EndDate,
            Position = job.Position,
            Commitment = job.Commitment,
            Remote = job.Remote,
            Localization = job.Localization,
            Education = job.Education,
            Experience = job.Experience,
            Language = job.Language,
            Description = job.Description,
            RequiredSkills = job.JobSkills
                .Where(js => js.IsRequired)
                .Select(js => js.Skill.SkillConvertToDto())
                .ToList(),
            NiceToHaveSkills = job.JobSkills
                .Where(js => !js.IsRequired)
                .Select(js => js.Skill.SkillConvertToDto())
                .ToList()
        };
    }

    public static Job DtoConvertToJob(this JobDto jobDto)
    {
        return new Job
        {
            JobId = jobDto.JobId,
            ClientId = jobDto.ClientId,
            EndDate = jobDto.EndDate,
            Position = jobDto.Position,
            Commitment = jobDto.Commitment,
            Remote = jobDto.Remote,
            Localization = jobDto.Localization,
            Education = jobDto.Education,
            Experience = jobDto.Experience,
            Language = jobDto.Language,
            Description = jobDto.Description,
            JobSkills = jobDto.RequiredSkills
                .Select(skillDto => new JobSkill
                {
                    JobId = jobDto.JobId,
                    SkillId = skillDto.SkillId,
                    IsRequired = true
                })
                .Concat(jobDto.NiceToHaveSkills
                    .Select(skillDto => new JobSkill
                    {
                        JobId = jobDto.JobId,
                        SkillId = skillDto.SkillId,
                        IsRequired = false
                    }))
                .ToList()
        };
    }
    
    public static IEnumerable<SkillDto> SkillsJobConvertToDto(this IEnumerable<Skill> skills)
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
}