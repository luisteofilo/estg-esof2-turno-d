using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context
{
    public partial class ApplicationDbContext
    {
        private void BuildInterviews(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interview>(entity =>
            {
                entity.HasKey(i => i.InterviewId);
                entity.HasOne(i => i.Feedback)
                    .WithOne(f => f.Interview)
                    .HasForeignKey<Feedback>(f => f.InterviewId)
                    .IsRequired(false);
            });
        }
    }
}