using System;
using ESOF.WebApp.DBLayer.Entities.Interviews;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class InterviewFeedback
    {
        public Guid InterviewFeedbackId { get; set; }
        public Candidate Candidate { get; set; }
        public Interview Interview { get; set; }
        public Interviewer Interviewer { get; set; }
        
        public string Feedback { get; set; }
        public string RejectionReason { get; set; }
        public string OptimizationSuggestions { get; set; }
        
        public Guid JobId { get; set; }  // Foreign key to Job
        public Job Job { get; set; }     // Navigation property
    }
}