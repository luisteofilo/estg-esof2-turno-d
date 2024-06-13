using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;
public partial class ApplicationDbContext : DbContext
    {
       
        private void BuildSkilVerticals(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<skil_veticals>()
                .Property(sv => sv.skil_veticalsId)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<skil_veticals>()
                .HasOne(sv => sv.RoleVerticals)
                .WithMany(rv => rv.Skills_verticals)
                .HasForeignKey(sv => sv.Role_verticalsId);

        }
    }
