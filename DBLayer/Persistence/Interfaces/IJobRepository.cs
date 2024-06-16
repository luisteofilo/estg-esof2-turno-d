
using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.DBLayer.Persistence.Interfaces;

public interface IJobRepository
{
    Task Create(Job job);
}