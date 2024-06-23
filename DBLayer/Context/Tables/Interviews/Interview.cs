using ESOF.WebApp.DBLayer.Entities.Interviews;
using Microsoft.EntityFrameworkCore;


namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildInterviews(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Interview>()
            .HasKey(e => new { e.InterviewId, e.InterviewerId,e.CandidateId});
    }
}