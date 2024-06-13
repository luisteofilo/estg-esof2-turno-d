using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Repositories;

public class TaxonomiasRep() : ITaxonomias
{
    public async Task<Vertical> GetTaxonomias()
    {
        var db = new ApplicationDbContext();
        
        var verticals = await db.Verticals
            .Include(p => p.VerticalName)
            .Include(p => p.Roles_verticals)
           
            // Hard coded Profile ID
            .FirstOrDefaultAsync(p => p.VerticalId == Guid.Parse("392fd8cc-e617-49d0-a2ac-885ee2f0155f"));

        return verticals;
    }
}