using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    
    private void BuildUserCompanies(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCompany>()
            .HasKey(uc => new { uc.UserId, uc.CompanyId });

        modelBuilder.Entity<UserCompany>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCompanies)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCompany>()
            .HasOne(uc => uc.Company)
            .WithMany(c => c.UserCompanies)
            .HasForeignKey(uc => uc.CompanyId);
    }
    
}