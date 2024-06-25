using ESOF.WebApp.DBLayer.Entities;

namespace ESOF.WebApp.WebAPI.Repositories.Contracts;

public interface ITaxonomias
{
    Task<IEnumerable<Vertical>> GetTaxonomias();
    Task<Vertical> GetTaxonomiaById(Guid id);
    void Update(Vertical vertical); 
    Task SaveAsync(); 
    Task Add(Vertical vertical);
    void Delete(Guid id);
}