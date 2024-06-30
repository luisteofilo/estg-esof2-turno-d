using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildTimesheets(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Timesheet>()
            .HasKey(t => t.TimesheetId);

        modelBuilder.Entity<Timesheet>()
            .HasOne(t => t.Position)
            .WithMany(p => p.Timesheets)
            .HasForeignKey(t => t.PositionId);

        modelBuilder.Entity<Timesheet>()
            .HasOne(t => t.Invoice)
            .WithOne(i => i.Timesheet)
            .HasForeignKey<Timesheet>(t => t.InvoiceId)
            .IsRequired(false);  // Permitir valores nulos para InvoiceId
    }
    
}