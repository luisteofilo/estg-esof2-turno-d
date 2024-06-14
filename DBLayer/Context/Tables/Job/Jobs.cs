using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class Jobs
{
    private void BuildJobs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>()
            .Property(p => p.JobId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Job>()
            .HasOne(j => j.Client)
            .WithMany(c => c.Jobs)
            .HasForeignKey(j => j.ClientId);

        modelBuilder.Entity<Job>()
            .HasMany(j => j.RequiredSkills)
            .WithOne(js => js.Job)
            .HasForeignKey(js => js.JobId);

        modelBuilder.Entity<Job>()
            .HasMany(j => j.NiceToHaveSkills)
            .WithOne(js => js.Job)
            .HasForeignKey(js => js.JobId);
    }
    
}