using Common.Dtos.Interview;
using Common.Dtos.Job;

namespace Common.Dtos.Optimization_Requests;

public class InterviewFeedbackDTO
{
    public Guid InterviewFeedbackId { get; set; }
    public Guid JobId { get; set; }
    public Guid Candidate { get; set; }
    public Guid Interview { get; set; }
    public Guid Interviewer { get; set; }
        
    public string Feedback { get; set; }
    public string RejectionReason { get; set; }
    public string OptimizationSuggestions { get; set; }
}