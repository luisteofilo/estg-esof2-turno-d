using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class PositionRepository : IPositionRepository
{
    private readonly ApplicationDbContext _context = new ApplicationDbContext();

    // Create a new position
    public async Task CreatePosition(Position position)
    {
        _context.Positions.Add(position);
        await _context.SaveChangesAsync();
    }

    // Get all positions
    public async Task<IEnumerable<Position>> GetAllPositions()
    {
        return await _context.Positions.Include(p => p.Job).Include(p => p.Timesheets).ToListAsync();
    }

    // Get a position by ID
    public async Task<Position> GetPositionById(Guid id)
    {
        return await _context.Positions.FindAsync(id);
    }

    // Update a position
    public async Task UpdatePosition(Position position)
    {
        _context.Positions.Update(position);
        await _context.SaveChangesAsync();
    }

    // Delete a position
    public async Task<bool> DeletePosition(Position position)
    {
        _context.Positions.Remove(position);
        await _context.SaveChangesAsync();
        return true;
    }
}