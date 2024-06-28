using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ESOF.WebApp.DBLayer.Entities.Emails;

namespace Frontend.Services
{
    public class EmailService
    {
        private readonly HttpClient _httpClient;

        public EmailService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ICollection<EmailTemplate>> GetAllTemplates()
        {
            var response = await _httpClient.GetAsync("http://localhost:5295/api/EmailTemplate");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ICollection<EmailTemplate>>();
        }

        public async Task<EmailTemplate> GetTemplateById(Guid emailTemplateId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5295/api/EmailTemplate/{emailTemplateId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmailTemplate>();
        }

        public async Task<EmailTemplate?> AddTemplate(EmailTemplate emailTemplate)
        {
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5295/api/EmailTemplate", emailTemplate);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmailTemplate>();
        }

        public async Task<EmailTemplate> UpdateTemplate(EmailTemplate emailTemplate, Guid emailTemplateId)
        {
            var response = await _httpClient.PutAsJsonAsync($"http://localhost:5295/api/EmailTemplate/{emailTemplateId}", emailTemplate);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmailTemplate>();
        }

        public async Task DeleteTemplate(Guid emailTemplateId)
        {
            var response = await _httpClient.DeleteAsync($"http://localhost:5295/api/EmailTemplate/{emailTemplateId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
