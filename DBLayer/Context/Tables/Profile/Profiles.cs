using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildProfiles(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Profile>()
            .Property(p => p.ProfileId)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<Profile>()
            .HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<Profile>(p => p.UserId);
        
    }
}