using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext : DbContext
{

    private void BuildSkillVerticals(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<skil_veticals>(entity =>
        {
            entity.Property(sv => sv.Role_verticalsId)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(sv => sv.RoleVerticals)
                .WithMany(rv => rv.Skills_verticals)
                .HasForeignKey(sv => sv.Role_verticalsId);
        });
    }
}
