using ESOF.WebApp.DBLayer.Entities.Interviews;
namespace WebAPI.Repositories.Contracts;
public interface IInterviewRepository
{
    Task<IEnumerable<Interview>> GetAllAsync();
    Task<Interview> GetByIdAsync(Guid interviewid);
    Task<bool> InterviewExistsAsync(Guid interviewid);
    Task AddAsync(Interview interview);
    Task UpdateAsync(Interview interview);
    Task<bool> InterviewerExistsAsync(Guid interviewerId);
    Task<bool> CandidateExistsAsync(Guid candidateId);

    Task<bool> IsInterviewerAndCandidateAvailableAsync(Guid interviewerId, Guid candidateId, DateTime start,
        DateTime end);

    bool IsCurrentDateTimeWithinInterview(Guid interviewId);
    DateTime GetCurrentDateTime();

    Task MarkPresenceAsync(Guid interviewId);
    Task UpdateInterviewStateAsync(Guid interviewId, InterviewState state);
    Task<bool> GetPresenceStateAsync(Guid interviewId);
}