using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildProfileSkills(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileSkill>()
            .HasKey(e => new { e.ProfileId, e.SkillId });
        
        modelBuilder.Entity<ProfileSkill>()
            .HasOne(ps => ps.Profile)
            .WithMany(p => p.ProfileSkills)
            .HasForeignKey(ps => ps.ProfileId);
    }
}