using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public class Clients
{
    private void BuildJobs(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .Property(c => c.ClientId)
            .HasDefaultValueSql("gen_random_uuid()");
    }

}