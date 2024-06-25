using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context.Optimization_Requests;


public partial class ApplicationDbContext
{
    private void BuildInterviewFeedback(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InterviewFeedback>()
            .HasKey(e => new {e.InterviewFeedbackId,e.JobId,e.Interview,e.Interviewer,e.Candidate,e.Feedback,e.RejectionReason,e.OptimizationSuggestions});
    }
}