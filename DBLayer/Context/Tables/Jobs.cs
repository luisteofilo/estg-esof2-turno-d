using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildJobs(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Job>()
            .Property(j => j.JobId)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}