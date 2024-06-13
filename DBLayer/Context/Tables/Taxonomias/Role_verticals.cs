using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildRoleVerticals(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role_verticals>()
            .Property(sv => sv.Role_verticalsId)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<Role_verticals>()
            .HasOne(rv => rv.Vertical)
            .WithMany(v => v.Roles_verticals)
            .HasForeignKey(rv => rv.VerticalId);

        

    }
}