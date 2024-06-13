
using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildverticalUser(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<verticalsUser>()
            .HasKey(e => new
            {
                e.UserId, e.VerticalId
            });
        
        modelBuilder.Entity<verticalsUser>()
            .HasOne(ps => ps.User)
            .WithMany(p => p.verticalsUser)
            .HasForeignKey(ps => ps.UserId);
        
        modelBuilder.Entity<verticalsUser>()
            .HasOne(ps => ps.Vertical)
            .WithMany(p => p.VerticalsUsers)
            .HasForeignKey(ps => ps.VerticalId);
    }
}
