using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.DBLayer.Persistence.Interfaces;

namespace ESOF.WebApp.DBLayer.Persistence.Repositories;

public class ImportRepository(ApplicationDbContext _dbContext) : BaseRepository(_dbContext), IImportRepository
{
    public async Task Create(Import import, CancellationToken cancellationToken)
    {
        await _dbContext.Imports.AddAsync(import, cancellationToken);
    }
}