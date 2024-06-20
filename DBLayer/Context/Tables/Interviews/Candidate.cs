using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.DBLayer.Context;

public partial class ApplicationDbContext
{
    private void BuildCandidates(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
            .HasMany(r => r.Interviews)
            .WithOne(ur => ur.Candidate)
            .HasForeignKey(ur => ur.CandidateId);
        
        modelBuilder.Entity<Candidate>()
            .Property(p => p.CandidateId)
            .HasDefaultValueSql("gen_random_uuid()");
    }
}