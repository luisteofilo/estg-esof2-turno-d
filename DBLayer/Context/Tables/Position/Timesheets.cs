using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildTimesheets(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Entities.Timesheet>()
            .Property(t => t.TimesheetId)
            .ValueGeneratedNever();

        modelBuilder.Entity<Entities.Timesheet>()
            .HasOne(t => t.Position)
            .WithMany(p => p.Timesheets)
            .HasForeignKey(t => t.PositionId);
    }
    
}