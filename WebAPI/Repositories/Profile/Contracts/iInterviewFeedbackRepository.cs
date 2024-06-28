using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.Interviews;

namespace ESOF.WebApp.WebAPI.Repositories;

public interface iInterviewFeedbackRepository
{
    Task<ICollection<InterviewFeedback>> GetInterviewsFeedbackAsync();
    Task<InterviewFeedback> GetInterviewFeedbackAsync(Guid id);
    Task<bool> InterviewFeedbackExistsAsync(Guid InterviewFeedbackId);
    Task<int> UpdateInterviewFeedbackAsync(InterviewFeedback InterviewFeedback);
    Task DeleteInterviewFeedbackAsync(Guid InterviewFeedbackId);
    Task AddInterviewFeedbackAsync(InterviewFeedback InterviewFeedback);
    Task<ICollection<InterviewFeedback>> GetInterviewFeedbackByJob(Guid JobId);
    Task<ICollection<InterviewFeedback>> GetInterviewFeedbackByCandidate(Guid candidate);
    Task<ICollection<InterviewFeedback>> GetInterviewFeedbackByInterview(Guid interview);
    Task<ICollection<InterviewFeedback>> GetInterviewFeedbackByInterviewer(Guid interview);



}