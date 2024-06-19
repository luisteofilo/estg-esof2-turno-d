
using System;

namespace ESOF.WebApp.DBLayer.Entities
{
    public class InterviewFeedback
    {
        public Guid InterviewFeedbackId { get; set; }
        public Guid JobId { get; set; }
        public string CandidateId { get; set; }
        public string Feedback { get; set; }
        public string RejectionReason { get; set; }
        public string OptimizationSuggestions { get; set; }

        public Job Job { get; set; }
    }
}