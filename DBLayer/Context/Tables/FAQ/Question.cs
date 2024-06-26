using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext 
{
    private void BuildFAQQuestions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers);

        modelBuilder.Entity<Question>()
            .Property(q => q.QuestionId)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}