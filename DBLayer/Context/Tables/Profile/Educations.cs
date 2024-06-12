using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildEducations(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Education>()
            .Property(p => p.EducationId)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<Education>()
            .HasOne(e => e.Profile)
            .WithMany(p => p.Educations)
            .HasForeignKey(e => e.ProfileId);
    }
}