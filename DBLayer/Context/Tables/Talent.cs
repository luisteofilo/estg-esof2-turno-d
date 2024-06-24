using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{

    private void BuildTalents(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Talent>()
            .Property(p => p.TalentId)
            .HasDefaultValueSql("gen_random_uuid()");
        
        modelBuilder.Entity<Talent>()
            .Property(p => p.Name)
            .IsRequired();
        
        modelBuilder.Entity<Talent>()
            .Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(15);
        
        modelBuilder.Entity<Talent>()
            .Property(p => p.Address)
            .IsRequired();

        modelBuilder.Entity<Talent>()
            .Property(p => p.City)
            .IsRequired();

        modelBuilder.Entity<Talent>()
            .Property(p => p.Country)
            .IsRequired();

        modelBuilder.Entity<Talent>()
            .Property(p => p.PostalCode)
            .IsRequired();
        
        modelBuilder.Entity<Talent>()
            .Property(p => p.AreaOfInterest)
            .IsRequired();
            
        modelBuilder.Entity<Talent>()
            .HasOne(t => t.User)
            .WithOne(u => u.Talent)
            .HasForeignKey<Talent>(t => t.UserId)
            .IsRequired();
    }
    
}