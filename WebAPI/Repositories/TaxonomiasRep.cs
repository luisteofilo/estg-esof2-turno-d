using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class TaxonomiasRep : ITaxonomias

{
    private readonly ApplicationDbContext db = new ApplicationDbContext();

    public async Task<Vertical?> GetTaxonomias()
    {
     
        var profileId = Guid.Parse("392fd8cc-e617-49d0-a2ac-885ee2f0155f");

        var verticals = await db.Verticals
            .Include(p => p.VerticalName)
            .Include(p => p.Roles_verticals)
            .FirstOrDefaultAsync(p => p.VerticalId == profileId);

        return verticals;
    }
    
    public async Task<Vertical?> GetTaxonomiaById(Guid id)
    {
        var vertical = await db.Verticals
            .Include(p => p.VerticalName)
            .Include(p => p.Roles_verticals)
            .FirstOrDefaultAsync(p => p.VerticalId == id);

        return vertical; // Returns Vertical or null
    }
    public void Update(Vertical vertical)
    {
        db.Entry(vertical).State = EntityState.Modified;
    }

    public async Task SaveAsync()
    {
        await db.SaveChangesAsync();
    }
    public async Task Add(Vertical vertical)
    {
        await db.Verticals.AddAsync(vertical);
    }
    public void Delete(Guid id)
    { 
        var vertical = new Vertical { VerticalId = id };
        db.Verticals.Remove(vertical);
    }
}
