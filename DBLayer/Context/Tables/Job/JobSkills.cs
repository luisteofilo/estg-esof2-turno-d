using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildJobSkills(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JobSkill>()
            .HasKey(js => new { js.JobId, js.SkillId });

        modelBuilder.Entity<JobSkill>()
            .HasOne(js => js.Job)
            .WithMany(j => j.JobSkills)
            .HasForeignKey(js => js.JobId);

        modelBuilder.Entity<JobSkill>()
            .HasOne(js => js.Skill)
            .WithMany(s => s.JobSkills)
            .HasForeignKey(js => js.SkillId);
    }
}