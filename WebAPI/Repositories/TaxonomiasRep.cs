using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;


public class TaxonomiasRep : ITaxonomias
{
    private readonly ApplicationDbContext _db = new ApplicationDbContext();

    public async Task<IEnumerable<Vertical>> GetTaxonomias()
    {
        var db = new ApplicationDbContext();
        return await db.Verticals
            .Include(v => v.Roles_verticals)
            .ThenInclude(rv => rv.Skills_verticals)
            .Include(v => v.VerticalsUsers)
            .ThenInclude(vu => vu.User)
            .ToListAsync();
    }

    public async Task<Vertical?> GetTaxonomiaById(Guid id)
    {
        return await _db.Verticals
            .Include(v => v.Roles_verticals)
            .ThenInclude(rv => rv.Skills_verticals)
            .Include(v => v.VerticalsUsers)
            .ThenInclude(vu => vu.User)
            .FirstOrDefaultAsync(v => v.VerticalId == id);
    }

    public async Task Add(Vertical vertical)
    {
        _db.Verticals.Add(vertical);
        await _db.SaveChangesAsync();
    }

    public async Task Update(Vertical vertical)
    {var db3 = new ApplicationDbContext();
        db3.Entry(vertical).State = EntityState.Modified;
        await db3.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {  var db2 = new ApplicationDbContext();
        var vertical = await db2.Verticals.FindAsync(id);
        if (vertical != null)
        {
            db2.Verticals.Remove(vertical);
            await db2.SaveChangesAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}