using ESOF.WebApp.DBLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts
{
    public interface IInterviewRepository
    { 
        Task<List<Interview>> GetAllInterviews(); 
    }
}