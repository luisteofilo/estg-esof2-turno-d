using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildClients(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .Property(c => c.ClientId)
            .HasDefaultValueSql("gen_random_uuid()");
    }

}