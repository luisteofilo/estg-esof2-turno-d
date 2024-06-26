using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface ITaxonomias
{
    Task<IEnumerable<Vertical>> GetTaxonomias();
    Task<Vertical> GetTaxonomiaById(Guid id);
    Task Update(Vertical vertical); 
    Task SaveAsync(); 
    Task Add(Vertical vertical);
    Task Delete(Guid id);
    Task<IEnumerable<Vertical>> GetTaxonomiasUsers(Guid id);
}