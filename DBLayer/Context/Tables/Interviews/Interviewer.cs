using ESOF.WebApp.DBLayer.Entities.Interviews;
using Microsoft.EntityFrameworkCore;


namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildInterviewer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Interviewer>()
            .HasMany(r => r.Interviews)
            .WithOne(ur => ur.Interviewer)
            .HasForeignKey(ur => ur.InterviewerId);
        
        modelBuilder.Entity<Interviewer>()
            .Property(p => p.InterviewerId)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}