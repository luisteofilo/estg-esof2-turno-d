using Common.Dtos.Interview;
using Common.Dtos.Job;
using Common.Dtos.Optimization_Requests;
using ESOF.WebApp.DBLayer.Entities.Interviews;

namespace Frontend.Services.Contracts
{
    public interface IInterviewFeedbackService
    {
        Task<ICollection<InterviewFeedbackDTO>> GetInterviewsFeedback();
        Task<InterviewFeedbackDTO> GetInterviewFeedback(Guid id);
        Task<InterviewFeedbackDTO> InterviewFeedbackExistsAsync(Guid InterviewFeedbackId);
        Task<InterviewFeedbackDTO> UpdateInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedbackDTO, Guid InterviewFeedbackId);
        Task DeleteInterviewFeedbackAsync(Guid InterviewFeedbackId);
        Task<InterviewFeedbackDTO> AddInterviewFeedbackAsync(InterviewFeedbackDTO interviewFeedbackDTO);
        Task<IEnumerable<CandidateDto>> GetCandidates();
        Task<IEnumerable<InterviewDto>> GetInterviews();
        Task<IEnumerable<InterviewerDto>> GetInterviewers();



        Task<JobDto?> UpdateJob(Guid JobId, JobDto jobDto);
    }
}