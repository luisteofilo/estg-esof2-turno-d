using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildPositions(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Position>()
            .Property(p => p.PositionId)
            .ValueGeneratedNever();

        modelBuilder.Entity<Entities.Position>()
            .HasOne(p => p.Job)
            .WithMany(j => j.Positions)
            .HasForeignKey(p => p.JobId);
    }
    
}