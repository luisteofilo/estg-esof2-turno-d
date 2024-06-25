using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;
using ESOF.WebApp.DBLayer.Entities;

namespace Frontend.Services.Contracts;

public interface IinterviewFeedback
{
    Task<ICollection<InterviewFeedbackDTO>> GetInterviewsFeedback();
    Task<InterviewFeedbackDTO> GetInterviewFeedback(Guid id);
    Task<InterviewFeedbackDTO?> InterviewFeedbackExistsAsync(Guid InterviewFeedbackId);
    Task<InterviewFeedbackDTO?> UpdateInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedbackDTO, Guid InterviewFeedbackId);
    Task DeleteInterviewFeedbackAsync(Guid InterviewFeedbackId);
    Task<InterviewFeedbackDTO?> AddInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedbackDTO);
    Task<InterviewFeedbackDTO?> GetInterviewFeedbackByJob(Guid JobId);
    Task<InterviewFeedbackDTO?> GetInterviewFeedbackByCandidate(Guid candidate);
    Task<InterviewFeedbackDTO?> GetInterviewFeedbackByInterview(Guid interview);
    Task<InterviewFeedbackDTO?> GetInterviewFeedbackByInterviewer(Guid interview);

    Task<JobDto?> UpdateJob(Guid JobId, JobDto jobDto);


}