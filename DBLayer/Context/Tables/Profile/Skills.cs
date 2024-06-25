using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildSkills(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skill>()
            .Property(p => p.SkillId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Skill>()
            .HasMany(s => s.JobSkills)
            .WithOne(js => js.Skill)
            .HasForeignKey(js => js.SkillId);
    }
}