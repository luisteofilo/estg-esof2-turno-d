using System.Net;
using System.Net.Mail;
using ESOF.WebApp.DBLayer.Entities.Emails;

namespace Frontend.Services;

public class EmailService2
{
    private readonly HttpClient _httpClient;

    private readonly SmtpClient _smtpClient;

    public EmailService2(HttpClient httpClient,
        IConfiguration configuration)
    {

        _httpClient = httpClient;
        _smtpClient = new SmtpClient(configuration["Smtp:Host"])
        {
            Port = int.Parse(configuration["Smtp:Port"]),
            Credentials = new NetworkCredential(configuration["Smtp:Username"], configuration["Smtp:Password"]),
            EnableSsl = bool.Parse(configuration["Smtp:EnableSsl"])
        };
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress("your-email@example.com"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(toEmail);

        await _smtpClient.SendMailAsync(mailMessage);
    }
    public async Task<ICollection<EmailTemplate>> GetAllTemplates()
    {
        var response = await _httpClient.GetAsync("http://localhost:5295/api/EmailTemplate");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<ICollection<EmailTemplate>>();
    }
    
    public async Task<EmailTemplate> GetTemplateById(Guid EmailTemplateId)
    {
        var response = await _httpClient.GetAsync($"http://localhost:5295/api/EmailTemplate/{EmailTemplateId}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EmailTemplate>();
    }
    
    public async Task<EmailTemplate?> AddTemplate(EmailTemplate emailTemplate)
    {
        var response = await _httpClient.PostAsJsonAsync("http://localhost:5295/api/EmailTemplate", emailTemplate);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EmailTemplate>();
    }
    public async Task<EmailTemplate> UpdateTemplate(EmailTemplate interviewFeedback, Guid InterviewFeedbackId)
    {
        var response = await _httpClient.PutAsJsonAsync($"http://localhost:5295/api/EmailTemplate/{InterviewFeedbackId}", interviewFeedback);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<EmailTemplate>();
    }
    
    public async Task DeleteTemplate(Guid InterviewFeedbackId)
    {
        var response = await _httpClient.DeleteAsync($"http://localhost:5295/api/EmailTemplate/{InterviewFeedbackId}");
        response.EnsureSuccessStatusCode();
    }
    
    
    

    
    
    
    
    
}