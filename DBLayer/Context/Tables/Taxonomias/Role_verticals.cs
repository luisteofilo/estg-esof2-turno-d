using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildRoleVerticals(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role_verticals>(entity =>
        {
            entity.Property(rv => rv.Role_verticalsId)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasOne(rv => rv.Vertical)
                .WithMany(v => v.Roles_verticals)
                .HasForeignKey(rv => rv.VerticalId);

            entity.HasMany(rv => rv.Skills_verticals)
                .WithOne(sv => sv.RoleVerticals)
                .HasForeignKey(sv => sv.Role_verticalsId);
        });
    }
}