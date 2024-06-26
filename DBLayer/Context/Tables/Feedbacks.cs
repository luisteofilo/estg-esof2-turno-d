using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context
{
    public partial class ApplicationDbContext
    {
        private void BuildFeedbacks(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.HasKey(f => f.FeedbackId);

                entity.HasIndex(f => f.Date);
                entity.HasIndex(f => f.Message);

                entity.Property(f => f.FeedbackId)
                    .HasDefaultValueSql("gen_random_uuid()")
                    .ValueGeneratedOnAdd();

                entity.HasOne(f => f.Interview)
                    .WithOne(i => i.Feedback)
                    .HasForeignKey<Feedback>(f => f.InterviewId)
                    .IsRequired();
            });
        }
    }
}