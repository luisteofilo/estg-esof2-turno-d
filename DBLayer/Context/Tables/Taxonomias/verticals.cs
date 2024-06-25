using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildVerticals(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vertical>(entity =>
        {
            entity.Property(p => p.VerticalId)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(v => v.Roles_verticals)
                .WithOne(r => r.Vertical)
                .HasForeignKey(r => r.VerticalId);

            entity.HasMany(v => v.VerticalsUsers)
                .WithOne(vu => vu.Vertical)
                .HasForeignKey(vu => vu.VerticalId);
        });
    }
}