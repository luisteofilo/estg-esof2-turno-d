using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities.Emails;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESOF.WebApp.WebAPI.Services
{
    public class EmailTemplateService
    {
        private readonly ApplicationDbContext db;

        public EmailTemplateService(ApplicationDbContext context)
        {
            db = context;
        }

        // Método para obter todos os templates
        public async Task<List<EmailTemplate>> GetAllTemplatesAsync()
        {
            return await db.EmailTemplates.ToListAsync();
        }

        // Método para obter um template específico pelo ID
        public async Task<EmailTemplate> GetTemplateByIdAsync(int id)
        {
            return await db.EmailTemplates.FindAsync(id);
        }

        // Método para adicionar um novo template
        public async Task AddTemplateAsync(EmailTemplate template)
        {
            db.EmailTemplates.Add(template);
            await db.SaveChangesAsync();
        }

        // Método para atualizar um template existente
        public async Task UpdateTemplateAsync(EmailTemplate template)
        {
            db.EmailTemplates.Update(template);
            await db.SaveChangesAsync();
        }

        // Método para deletar um template pelo ID
        public async Task DeleteTemplateAsync(int id)
        {
            var template = await db.EmailTemplates.FindAsync(id);
            if (template != null)
            {
                db.EmailTemplates.Remove(template);
                await db.SaveChangesAsync();
            }
        }
    }
}