using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildSlot(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Slot>()
            .HasKey(e => new { e.SlotId, e.InterviewerId, e.InterviewId});
    }
}