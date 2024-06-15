namespace Common.Dtos.Job;
using ESOF.WebApp.DBLayer.Entities;

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
            EndDate = job.EndDate,
            Position = job.Position,
            Commitment = job.Commitment,
            Remote = job.Remote,
            Localization = job.Localization,
            Education = job.Education,
            RequiredSkills = job.RequiredSkills,
            NiceToHaveSkills = job.NiceToHaveSkills,
            Experience = job.Experience,
            Language = job.Language,
            Description = job.Description
        };
    }

    public static Job DtoConvertToJob(this JobDto jobDto)
    {
        return new Job
        {
            JobId = jobDto.JobId,
            EndDate = jobDto.EndDate,
            Position = jobDto.Position,
            Commitment = jobDto.Commitment,
            Remote = jobDto.Remote,
            Localization = jobDto.Localization,
            Education = jobDto.Education,
            RequiredSkills = jobDto.RequiredSkills,
            NiceToHaveSkills = jobDto.NiceToHaveSkills,
            Experience = jobDto.Experience,
            Language = jobDto.Language,
            Description = jobDto.Description
        };
    }
}