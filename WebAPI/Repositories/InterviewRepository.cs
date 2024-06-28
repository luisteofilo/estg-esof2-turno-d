using Microsoft.EntityFrameworkCore;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using WebAPI.Repositories.Contracts;


namespace WebAPI.Repositories;
public class InterviewRepository : IInterviewRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    public async Task<IEnumerable<Interview>> GetAllAsync(string candidate = null, string location = null)
    {
        var query = _context.Interviews.AsQueryable();
        
        if (!string.IsNullOrEmpty(candidate))
        {
            query = query.Where(i => i.Candidate.Name.ToLower() == candidate.ToLower());
        }

        if (!string.IsNullOrEmpty(location))
        {
            query = query.Where(i => i.Location.ToLower() == location.ToLower());
        }
        
        return await query.OrderBy(p => p.InterviewId).ToListAsync();
    }

    public async Task<Interview> GetByIdAsync(Guid interviewId)
    {
        return await _context.Interviews.FirstOrDefaultAsync(p => p.InterviewId == interviewId);
    }

    public async Task<bool> InterviewExistsAsync(Guid interviewId)
    {
        return await _context.Interviews.AnyAsync(p => p.InterviewId == interviewId);
    }

    public async Task AddAsync(Interview interview)
    {
        await _context.Interviews.AddAsync(interview);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Interview interview)
    {
        _context.Interviews.Update(interview);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> InterviewerExistsAsync(Guid interviewerId)
    {
        return await _context.Interviewers.AnyAsync(i => i.InterviewerId == interviewerId);
    }

    public async Task<bool> CandidateExistsAsync(Guid candidateId)
    {
        return await _context.Candidates.AnyAsync(i => i.CandidateId == candidateId);
    }

    public async Task<bool> IsInterviewerAndCandidateAvailableAsync(Guid interviewerId, Guid candidateId, DateTime start, DateTime end)
    {
        var interviews = await _context.Interviews
            .AsNoTracking()
            .Where(i => (i.InterviewerId == interviewerId || i.CandidateId == candidateId) &&
                        (i.InterviewState == InterviewState.Scheduled || i.InterviewState == InterviewState.OnGoing))
            .ToListAsync();

        foreach (var interview in interviews)
        {
            if ((start < interview.DateHourEnd.AddMinutes(30) && end > interview.DateHourStart.AddMinutes(-30)))
            {
                return false;
            }
        }

        return true;
    }

    public DateTime GetCurrentDateTime()
    {
        return DateTime.Now;
    }

    public bool IsCurrentDateTimeWithinInterview(Guid interviewId)
    {
        var interview = _context.Interviews
            .AsNoTracking()
            .FirstOrDefault(i => i.InterviewId == interviewId);

        if (interview == null)
        {
            throw new ArgumentException("Entrevista não encontrada.");
        }

        var currentTime = DateTime.Now;
        return currentTime >= interview.DateHourStart && currentTime <= interview.DateHourEnd;
    }

    public async Task MarkPresenceAsync(Guid interviewId)
    {
        var interview = await GetByIdAsync(interviewId);
        if (interview != null)
        {
            interview.PresenceMarked = true;
            await UpdateAsync(interview);
        }
    }

    public async Task UpdateInterviewStateAsync(Guid interviewId, InterviewState state)
    {
        var interview = await GetByIdAsync(interviewId);
        if (interview != null)
        {
            interview.InterviewState = state;
            await UpdateAsync(interview);
        }
    }
    
    public async Task<bool> GetPresenceStateAsync(Guid interviewId)
    {
        var interview = await _context.Interviews.FirstOrDefaultAsync(p => p.InterviewId == interviewId);
        if (interview != null)
        {
            return interview.PresenceMarked;
        }
        return false;
    }

    public async Task<List<Candidate>> GetAllInterviewCandidates()
    {
        return await _context.Interviews
            .Where(i => i.Candidate != null)
            .Select(i => i.Candidate)
            .Distinct()
            .ToListAsync();
    }
    
    public async Task<List<string>> GetAllInterviewLocation()
    {
        return await _context.Interviews
            .Where(i => i.Location != null)
            .Select(i => i.Location)
            .Distinct()
            .ToListAsync();
    }

}

