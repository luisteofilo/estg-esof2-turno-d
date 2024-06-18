using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private const string JobTable = "Jobs";

    private void BuildJobs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>().ToTable(JobTable);

        modelBuilder.Entity<Job>()
            .Property(j => j.JobId)
            .ValueGeneratedNever();

        modelBuilder.Entity<Job>()
            .HasKey(key => key.JobId);
    }
}