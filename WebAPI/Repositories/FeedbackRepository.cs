using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;


namespace ESOF.WebApp.WebAPI.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {

        public async Task<Feedback> GetFeedbackByInterviewIdAsync(Guid interviewId)
        {
            var db = new ApplicationDbContext();

            return await db.Feedbacks
                .FirstOrDefaultAsync(f => f.InterviewId == interviewId);
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            var db = new ApplicationDbContext();
            await db.Feedbacks.AddAsync(feedback);
            await db.SaveChangesAsync();
        }
    }
}