namespace Common.Dtos.Job;
using ESOF.WebApp.DBLayer.Entities;
using Common.Dtos.Profile;

public static class DtoConversion
{
    public static IEnumerable<JobDto> JobsConvertToDto(this IEnumerable<Job> jobs)
    {
        return jobs.Select(job => job.JobConvertToDto()).ToList();
    }
    
    public static JobDto CopyJobDto(this Job job)
    {
        return new JobDto
        {
            JobId = job.JobId,
            ClientId = job.ClientId,
            EndDate = job.EndDate,
            Positions = job.Positions,
            Commitment = job.Commitment,
            Remote = job.Remote,
            Localization = job.Localization,
            Education = job.Education,
            Experience = job.Experience,
            Description = job.Description
        };
    }
    
    public static JobDto JobConvertToDto(this Job job)
    {
        return new JobDto
        {
            JobId = job.JobId,
            ClientId = job.ClientId,
            EndDate = job.EndDate,
            Positions = job.Positions,
            Commitment = job.Commitment,
            Remote = job.Remote,
            Localization = job.Localization,
            Education = job.Education,
            Experience = job.Experience,
            Description = job.Description
        };
    }

    public static Job DtoConvertToJob(this JobDto jobDto)
    {
        
        if (jobDto.EndDate.HasValue) 
        {
            DateTime endDateWithKind = DateTime.SpecifyKind(jobDto.EndDate.Value, DateTimeKind.Utc);
            jobDto.EndDate = endDateWithKind;
        }
        
        return new Job
        {
            ClientId = jobDto.ClientId,
            EndDate = jobDto.EndDate,
            Positions = jobDto.Positions,
            Commitment = jobDto.Commitment,
            Remote = jobDto.Remote,
            Localization = jobDto.Localization,
            Education = jobDto.Education,
            Experience = jobDto.Experience,
            Description = jobDto.Description
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
    
    public static IEnumerable<JobSkillDto> JobSkillsConvertToDto(this IEnumerable<JobSkill> jobSkills)
    {
        return jobSkills.Select(jobSkill => jobSkill.JobSkillConvertToDto()).ToList();
    }
    
    public static JobSkillDto JobSkillConvertToDto(this JobSkill jobSkill)
    {
        return new JobSkillDto()
        {
            JobId = jobSkill.JobId,
            SkillId = jobSkill.SkillId,
            IsRequired = jobSkill.IsRequired
        };
    }
    
    public static JobSkill DtoConvertTOJobSkill(this JobSkillDto jobSkillDto)
    {
        return new JobSkill
        {
            JobId = jobSkillDto.JobId,
            SkillId = jobSkillDto.SkillId,
            IsRequired = jobSkillDto.IsRequired
        };
    }
}