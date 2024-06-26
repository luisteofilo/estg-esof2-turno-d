using Common.Dtos.Feedback;
using System;
using System.Threading.Tasks;

namespace Frontend.Services.Contracts
{
    public interface IFeedbackService
    {
        Task<FeedbackDto> GetFeedbackByInterviewIdAsync(Guid interviewId);
        Task SubmitFeedbackAsync(FeedbackDto feedbackDto);
    }
}