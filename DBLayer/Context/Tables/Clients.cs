using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildClients(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .Property(p => p.ClientId)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<Client>()
            .Property(p => p.CompanyName)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(p => p.Phone)
            .IsRequired()
            .HasMaxLength(15);

        modelBuilder.Entity<Client>()
            .Property(p => p.Address)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(p => p.City)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(p => p.Country)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(p => p.PostalCode)
            .IsRequired();

        modelBuilder.Entity<Client>()
            .Property(p => p.CompanyDescription)
            .IsRequired();
        
        modelBuilder.Entity<Client>()
            .HasOne(c => c.User)
            .WithOne(u => u.Client)
            .HasForeignKey<Client>(c => c.UserId)
            .IsRequired();
    }
}