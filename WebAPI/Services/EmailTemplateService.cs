using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities.Emails;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Services
{
    public class EmailTemplateService
    {
        // Método para obter todos os templates
        public async Task<List<EmailTemplate>> GetAllTemplatesAsync()
        {
            var _db = new ApplicationDbContext();
            
            return await _db.EmailTemplates.ToListAsync();
        }

        // Método para obter um template específico pelo ID
        public async Task<EmailTemplate?> GetTemplateByIdAsync(int id)
        {
            var _db = new ApplicationDbContext();
            
            return await _db.EmailTemplates.FindAsync(id);
        }

        // Método para adicionar um novo template
        public async Task AddTemplateAsync(EmailTemplate template)
        {
            var _db = new ApplicationDbContext();
            
            _db.EmailTemplates.Add(template);
            await _db.SaveChangesAsync();
        }

        // Método para atualizar um template existente
        public async Task UpdateTemplateAsync(EmailTemplate template)
        {
            var _db = new ApplicationDbContext();
            
            _db.EmailTemplates.Update(template);
            await _db.SaveChangesAsync();
        }

        // Método para deletar um template pelo ID
        public async Task DeleteTemplateAsync(int id)
        {
            var _db = new ApplicationDbContext();
            var template = await _db.EmailTemplates.FindAsync(id);
            
            if (template != null)
            {
                _db.EmailTemplates.Remove(template);
                await _db.SaveChangesAsync();
            }
        }
    }
}