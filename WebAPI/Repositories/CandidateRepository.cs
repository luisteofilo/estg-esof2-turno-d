using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Repositories;
public class CandidateRepository : ICandidateRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task<IEnumerable<Candidate>> GetAllAsync()
    {
        return await _context.Candidates.OrderBy(p => p.CandidateId).ToListAsync();
    }

    public async Task<Candidate> GetByIdAsync(Guid candidateid)
    {
        return await _context.Candidates.FirstOrDefaultAsync(c => c.CandidateId == candidateid);
    }

    public async Task AddAsync(Candidate candidate)
    {
        await _context.Candidates.AddAsync(candidate);
        await _context.SaveChangesAsync();
    }
}
