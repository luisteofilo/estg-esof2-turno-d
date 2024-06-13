using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface ITaxonomias
{
    Task<Vertical> GetTaxonomias();
}