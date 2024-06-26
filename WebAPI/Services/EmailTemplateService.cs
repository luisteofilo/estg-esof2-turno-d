using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities.Emails;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.WebAPI.Services
{
    public class EmailTemplateService
    {

        private readonly ApplicationDbContext _db;

        public EmailTemplateService(ApplicationDbContext db)
        {
            _db = db;
        }

        private readonly ApplicationDbContext _db = new ApplicationDbContext();


        // Método para obter todos os templates
        public async Task<List<EmailTemplate>> GetAllTemplatesAsync()
        {
            return await _db.EmailTemplates.ToListAsync();
        }

        // Método para obter um template específico pelo ID
        public async Task<EmailTemplate?> GetTemplateByIdAsync(int id)
        {
            return await _db.EmailTemplates.FindAsync(id);
        }

        // Método para adicionar um novo template
        public async Task AddTemplateAsync(EmailTemplate template)
        {
            _db.EmailTemplates.Add(template);
            await _db.SaveChangesAsync();
        }

        // Método para atualizar um template existente
        public async Task UpdateTemplateAsync(EmailTemplate template)
        {
            _db.EmailTemplates.Update(template);
            await _db.SaveChangesAsync();
        }

        // Método para deletar um template pelo ID
        public async Task DeleteTemplateAsync(int id)
        {
            var template = await _db.EmailTemplates.FindAsync(id);
            if (template != null)
            {
                _db.EmailTemplates.Remove(template);
                await _db.SaveChangesAsync();
            }
        }
    }
}