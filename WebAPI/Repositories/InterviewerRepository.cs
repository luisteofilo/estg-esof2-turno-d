using Microsoft.EntityFrameworkCore;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using WebAPI.Repositories.Contracts;


namespace WebAPI.Repositories;
public class InterviewerRepository : IInterviewerRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task<IEnumerable<Interviewer>> GetAllAsync()
    {
        return await _context.Interviewers.OrderBy(p => p.InterviewerId).ToListAsync();
    }

    public async Task<Interviewer> GetByIdAsync(Guid interviewerid)
    {
        return await _context.Interviewers.FirstOrDefaultAsync(i => i.InterviewerId == interviewerid);
    }

    public async Task AddAsync(Interviewer interviewer)
    {
        await _context.Interviewers.AddAsync(interviewer);
        await _context.SaveChangesAsync();
    }
}
