using ESOF.WebApp.DBLayer.Entities.Interviews;

namespace WebAPI.Repositories.Contracts;
public interface ICandidateRepository
{
    Task<IEnumerable<Candidate>> GetAllAsync();
    Task<Candidate> GetByIdAsync(Guid candidateid);
    Task AddAsync(Candidate candidate);
}


