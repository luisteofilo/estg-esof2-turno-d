using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos.Interview;

namespace Frontend.Services.Contracts
{
    public interface IInterviewerService
    {
        Task<IEnumerable<InterviewerDto>> GetInterviewersAsync();
        Task CreateInterviewerAsync(InterviewerDto interviewer);
    }
}