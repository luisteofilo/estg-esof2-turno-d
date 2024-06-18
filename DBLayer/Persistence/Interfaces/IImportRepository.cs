
using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.DBLayer.Persistence.Interfaces;

public interface IImportRepository
{
    Task Create(Import import, CancellationToken cancellationToken);
}