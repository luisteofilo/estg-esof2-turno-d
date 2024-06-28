using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.Interviews;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IInterviewFeedback
{
    Task<ICollection<InterviewFeedback>> GetInterviewsFeedbackAsync();
    Task<InterviewFeedback> GetInterviewFeedbackAsync(Guid id);
    Task<bool> InterviewFeedbackExistsAsync(Guid InterviewFeedbackId);
    Task<int> UpdateInterviewFeedbackAsync(InterviewFeedback InterviewFeedback);
    Task DeleteInterviewFeedbackAsync(Guid InterviewFeedbackId);
    Task AddInterviewFeedbackAsync(InterviewFeedback InterviewFeedback);
    
    Task<int> UpdateJob(Job job, Guid JobId);
    


}