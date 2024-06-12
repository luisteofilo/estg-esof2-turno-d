using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildExperiences(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Experience>()
            .Property(p => p.ExperienceId)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<Experience>()
            .HasOne(e => e.Profile)
            .WithMany(p => p.Experiences)
            .HasForeignKey(e => e.ProfileId);
    }
}