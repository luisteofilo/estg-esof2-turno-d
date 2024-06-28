using Common.Dtos.Job;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Entities.Interviews;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public class InterviewFeedbackRepository : IInterviewFeedback
{
     private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
     private readonly JobRepository _repository;

    public async Task<ICollection<InterviewFeedback>> GetInterviewsFeedbackAsync()
    {
        return await _dbContext.InterviewFeedbacks.OrderBy(p => p.InterviewFeedbackId).ToListAsync();
    }
    
    public async Task<InterviewFeedback> GetInterviewFeedbackAsync(Guid id)
    {
        return await _dbContext.InterviewFeedbacks.FirstOrDefaultAsync(e => e.InterviewFeedbackId == id);
    }

    
    public async Task<bool> InterviewFeedbackExistsAsync(Guid InterviewFeedbackId)
    {
        return await _dbContext.InterviewFeedbacks.AnyAsync(e => e.InterviewFeedbackId == InterviewFeedbackId);
    }
    
    public async Task<int> UpdateInterviewFeedbackAsync(InterviewFeedback InterviewFeedback)
    {
        _dbContext.InterviewFeedbacks.Update(InterviewFeedback);
        return await _dbContext.SaveChangesAsync();
    }
    

    
    public async Task<ICollection<Job>> GetJobByInterviewFeedback()
    {
        return (ICollection<Job>)await _repository.GetJobsAsync();
    }

    public async Task DeleteInterviewFeedbackAsync(Guid InterviewFeedbackId)
    {
        var interviewfeedback = await _dbContext.InterviewFeedbacks.FindAsync(InterviewFeedbackId);

        if (interviewfeedback != null)
        {
            _dbContext.InterviewFeedbacks.Remove(interviewfeedback);
            await _dbContext.SaveChangesAsync();
        }
    }
    
    public async Task AddInterviewFeedbackAsync(InterviewFeedback interviewfeedback)
    {
        await _dbContext.InterviewFeedbacks.AddAsync(interviewfeedback);
        await _dbContext.SaveChangesAsync();
    }


    public async Task<int> UpdateJob(Job job,Guid JobId)
    {
        job.UpdatedAt = DateTimeOffset.UtcNow;
        _dbContext.Jobs.Update(job);
        return await _dbContext.SaveChangesAsync();
    }
}