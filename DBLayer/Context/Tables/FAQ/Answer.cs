using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildFAQAnswers(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Author);

        modelBuilder.Entity<Answer>()
            .HasOne(q => q.Question);

        modelBuilder.Entity<Answer>()
            .Property(a => a.AnswerId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Answer>()
            .Property(a => a.CreatedAt)
            .HasDefaultValueSql("now()");
    }
}