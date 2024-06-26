using ESOF.WebApp.DBLayer.Context;

namespace ESOF.WebApp.DBLayer.Persistence.Repositories;

public class BaseRepository
{

    protected readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

}