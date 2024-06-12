using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildCompanies(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Companies>()
            .Property(c => c.CompanieId)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<Companies>()
            .HasIndex(c => c.Name)
            .IsUnique();
    }
}