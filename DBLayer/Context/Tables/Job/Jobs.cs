using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildJobs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>()
            .Property(p => p.JobId)
            .ValueGeneratedNever();

        modelBuilder.Entity<Job>()
            .HasMany(j => j.JobSkills)
            .WithOne(js => js.Job)
            .HasForeignKey(js => js.JobId);

        modelBuilder.Entity<Job>()
           .HasKey(key => key.JobId);
    }

}