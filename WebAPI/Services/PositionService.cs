using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.Services
{
    public class PositionService
    {
        private readonly ApplicationDbContext _context;

        public PositionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new position
        public async Task<Position> CreatePosition(Position position)
        {
            _context.Positions.Add(position);
            await _context.SaveChangesAsync();
            return position;
        }

        // Get all positions
        public async Task<IEnumerable<Position>> GetAllPositions()
        {
            return await _context.Positions.Include(p => p.Job).Include(p => p.Timesheets).ToListAsync();
        }

        // Get a position by ID
        public async Task<Position> GetPositionById(int id)
        {
            return await _context.Positions.Include(p => p.Job).Include(p => p.Timesheets).FirstOrDefaultAsync(p => p.PositionId == id);
        }

        // Update a position
        public async Task<Position> UpdatePosition(Position position)
        {
            _context.Positions.Update(position);
            await _context.SaveChangesAsync();
            return position;
        }

        // Delete a position
        public async Task<bool> DeletePosition(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position == null)
            {
                return false;
            }

            _context.Positions.Remove(position);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}