using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.FAQ;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildFAQJobs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.FAQ.Job>()
            .Property(a => a.JobId)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}