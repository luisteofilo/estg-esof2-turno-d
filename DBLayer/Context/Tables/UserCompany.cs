using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildUserCompanies(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCompany>()
            .Property(uc => uc.UserCompanyId)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<UserCompany>()
            .HasIndex(uc => new { uc.UserId, uc.CompanyId })
            .IsUnique();
    }
}