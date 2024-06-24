using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Dtos.Interview;

namespace Frontend.Services.Contracts
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateDto>> GetCandidatesAsync();
        Task CreateCandidateAsync(CandidateDto candidate);
    }
}