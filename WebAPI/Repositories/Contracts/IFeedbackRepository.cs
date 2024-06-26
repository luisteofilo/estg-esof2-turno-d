using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IFeedbackRepository
{
    Task<Feedback> GetFeedbackByInterviewIdAsync(Guid interviewId);
    Task AddFeedbackAsync(Feedback feedback);
}