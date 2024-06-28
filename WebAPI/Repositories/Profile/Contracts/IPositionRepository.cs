using Common.Dtos.Position;
using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface IPositionRepository
{
    Task CreatePosition(Position position);

    // Get all positions
    Task<IEnumerable<Position>> GetAllPositions();

    // Get a position by ID
    Task<Position> GetPositionById(Guid id);
    
    // Update a position
    Task UpdatePosition(Position position);
    
    // Delete a position
    Task<bool> DeletePosition(Position position);

}