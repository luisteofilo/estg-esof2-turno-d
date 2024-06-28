using ESOF.WebApp.DBLayer.Entities.Interviews;
namespace WebAPI.Repositories.Contracts;
public interface IInterviewerRepository
{
    Task<IEnumerable<Interviewer>> GetAllAsync();
    Task<Interviewer> GetByIdAsync(Guid interviewerid);
    Task AddAsync(Interviewer interviewer);
}
