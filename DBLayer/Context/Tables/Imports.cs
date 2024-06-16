using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildImports(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Import>()
            .HasMany(j => j.Jobs)
            .WithOne(j => j.Import)
            .HasForeignKey(j => j.ImportId);

        modelBuilder.Entity<Import>()
            .Property(i => i.ImportId)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}