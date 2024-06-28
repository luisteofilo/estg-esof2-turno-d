using Common.Dtos.Interview;
using ESOF.WebApp.DBLayer.Entities;

namespace Common.Dtos.Optimization_Requests;

public static class DtoConversion
{
    
    public static IEnumerable<InterviewFeedbackDTO> InterviewsçFeesbackConvertToDto(this IEnumerable<InterviewFeedback> interviews)
    {
        return interviews.Select(InterviewFeedback => InterviewFeedback.InterviewFeedbackConvertToDto()).ToList();
    }

    public static InterviewFeedbackDTO CopyInterviewFeedbackDto(this InterviewFeedbackDTO interviewFeedbackDto)
    {
        return new InterviewFeedbackDTO()
        {
            InterviewFeedbackId = interviewFeedbackDto.InterviewFeedbackId,
            JobId = interviewFeedbackDto.JobId,
            Candidate = interviewFeedbackDto.Candidate,
            Interview = interviewFeedbackDto.Interview,
            Interviewer = interviewFeedbackDto.Interviewer,
            Feedback = interviewFeedbackDto.Feedback,
            RejectionReason = interviewFeedbackDto.RejectionReason,
            OptimizationSuggestions = interviewFeedbackDto.OptimizationSuggestions,
            
            
        };
    }
    
    public static InterviewFeedbackDTO InterviewFeedbackConvertToDto(this InterviewFeedback interviewFeedback)
    {
        return new InterviewFeedbackDTO()
        {
            InterviewFeedbackId = interviewFeedback.InterviewFeedbackId,
            JobId = interviewFeedback.JobId,
            Candidate = interviewFeedback.Candidate,
            Interview = interviewFeedback.Interview,
            Interviewer = interviewFeedback.Interviewer,
            Feedback = interviewFeedback.Feedback,
            RejectionReason = interviewFeedback.RejectionReason,
            OptimizationSuggestions = interviewFeedback.OptimizationSuggestions,
            
        };
    }
    public static InterviewFeedback DtoConvertToInterviewFeedback(this InterviewFeedbackDTO interviewFeedbackDto)
    {
        return new InterviewFeedback()
        {
            InterviewFeedbackId = interviewFeedbackDto.InterviewFeedbackId,
            JobId = interviewFeedbackDto.JobId,
            Candidate = interviewFeedbackDto.Candidate,
            Interview = interviewFeedbackDto.Interview,
            Interviewer = interviewFeedbackDto.Interviewer,
            Feedback = interviewFeedbackDto.Feedback,
            RejectionReason = interviewFeedbackDto.RejectionReason,
            OptimizationSuggestions = interviewFeedbackDto.OptimizationSuggestions,
        };
    }
    
    
}