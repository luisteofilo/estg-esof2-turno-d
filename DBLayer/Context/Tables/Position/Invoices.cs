using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildInvoices(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>()
            .HasKey(i => i.InvoiceId);

        modelBuilder.Entity<Invoice>()
            .HasOne(i => i.Timesheet)
            .WithOne(t => t.Invoice)
            .HasForeignKey<Invoice>(i => i.TimesheetId);
    }
}