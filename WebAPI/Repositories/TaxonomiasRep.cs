using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;


namespace ESOF.WebApp.WebAPI.Repositories
{
    public class TaxonomiasRep : ITaxonomias
    {
        private readonly ApplicationDbContext _db;
        

        public async Task<Vertical?> GetTaxonomias()
        {
            try
            {
                var Id = Guid.Parse("392fd8cc-e617-49d0-a2ac-885ee2f0155f");

                var vertical = await _db.Verticals
                    .Include(v => v.Roles_verticals)
                    .Include(v => v.VerticalsUsers)
                    .FirstOrDefaultAsync(v => v.VerticalId == Id);

                return vertical;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception("Error retrieving taxonomias from the database.", ex);
            }
        }

        public async Task<Vertical?> GetTaxonomiaById(Guid id)
        {
            try
            {
                var vertical = await _db.Verticals
                    .Include(v => v.Roles_verticals)
                    .Include(v => v.VerticalsUsers)
                    .FirstOrDefaultAsync(v => v.VerticalId == id);

                return vertical;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception($"Error retrieving taxonomia with id {id} from the database.", ex);
            }
        }

        public void Update(Vertical vertical)
        {
            try
            {
                _db.Entry(vertical).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception($"Error updating taxonomia with id {vertical.VerticalId}.", ex);
            }
        }

        public async Task SaveAsync()
        {
            try
            {
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception("Error saving changes to the database.", ex);
            }
        }

        public async Task Add(Vertical vertical)
        {
            try
            {
                await _db.Verticals.AddAsync(vertical);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception("Error adding new taxonomia to the database.", ex);
            }
        }

        public void Delete(Guid id)
        {
            try
            {
                var vertical = new Vertical { VerticalId = id };
                _db.Verticals.Remove(vertical);
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                throw new Exception($"Error deleting taxonomia with id {id} from the database.", ex);
            }
        }
    }
}