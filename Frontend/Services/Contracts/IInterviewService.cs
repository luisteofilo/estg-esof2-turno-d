using Common.Dtos.Interview;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using ESOF.WebApp.DBLayer.Entities.Interviews;

namespace Frontend.Services.Contracts
{
    public interface IInterviewService
    {
        Task<InterviewDto> CreateInterviewAsync(InterviewDto interviewDto);
        Task<IEnumerable<InterviewDto>> GetInterviewsAsync();
        Task CancelInterviewAsync(Guid interviewId);
        Task MarkPresenceAsync(Guid interviewId);
        Task UpdateInterviewStateAsync(Guid interviewId, InterviewState state);
        Task<DateTime> GetCurrentDateTimeAsync();
        Task<bool> GetPresenceMarkedAsync(Guid interviewId);
    }
}