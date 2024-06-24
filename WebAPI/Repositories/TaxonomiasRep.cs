using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class TaxonomiasRep : ITaxonomias

{
   
    public async Task<Vertical?> GetTaxonomias()
    {
        var db = new ApplicationDbContext();
        // Hard coded Profile ID
        var profileId = Guid.Parse("392fd8cc-e617-49d0-a2ac-885ee2f0155f");

        var verticals = await db.Verticals
            .Include(p => p.VerticalName)
            .Include(p => p.Roles_verticals)
            .FirstOrDefaultAsync(p => p.VerticalId == profileId);

        return verticals;
    }
    
    public async Task<Vertical?> GetTaxonomiaById(Guid id)
    { var db = new ApplicationDbContext();
        var vertical = await db.Verticals
            .Include(p => p.VerticalName)
            .Include(p => p.Roles_verticals)
            .FirstOrDefaultAsync(p => p.VerticalId == id);

        return vertical; // Returns Vertical or null
    }
    public void Update(Vertical vertical)
    { var db = new ApplicationDbContext();
        db.Entry(vertical).State = EntityState.Modified;
    }

    public async Task SaveAsync()
    { var db = new ApplicationDbContext();
        await db.SaveChangesAsync();
    }
    public async Task Add(Vertical vertical)
    {var db = new ApplicationDbContext();
        await db.Verticals.AddAsync(vertical);
    }
    public void Delete(Guid id)
    { var db = new ApplicationDbContext();
        var vertical = new Vertical { VerticalId = id };
        db.Verticals.Remove(vertical);
    }
}
