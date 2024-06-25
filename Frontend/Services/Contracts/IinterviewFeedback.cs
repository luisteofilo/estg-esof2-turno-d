using ESOF.WebApp.DBLayer.Entities;

namespace Frontend.Services.Contracts;

public interface IinterviewFeedback
{
    Task<ICollection<InterviewFeedback>> GetInterviewsFeedback();
    Task<InterviewFeedback> GetInterviewFeedback(Guid id);
    Task<InterviewFeedback?> InterviewFeedbackExistsAsync(Guid InterviewFeedbackId);
    Task<InterviewFeedback?> UpdateInterviewFeedbackAsync(InterviewFeedback InterviewFeedback, Guid InterviewFeedbackId);
    Task DeleteInterviewFeedbackAsync(Guid InterviewFeedbackId);
    Task<InterviewFeedback?> AddInterviewFeedbackAsync(InterviewFeedback InterviewFeedback);
    Task<InterviewFeedback?> GetInterviewFeedbackByJob(Guid JobId);
    Task<InterviewFeedback?> GetInterviewFeedbackByCandidate(Guid candidate);
    Task<InterviewFeedback?> GetInterviewFeedbackByInterview(Guid interview);
    Task<InterviewFeedback?> GetInterviewFeedbackByInterviewer(Guid interview);


}