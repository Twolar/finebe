using finebe.webapi.Src.Helpers;
using finebe.webapi.Src.Interfaces;
using finebe.webapi.Src.Models.Settings;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace finebe.webapi.Src.Services;

public class EmailService : IEmailService
{
    private readonly IOptions<SendGridSettings> _sendGridSettings;

    public EmailService(IOptions<SendGridSettings> sendGridSettings)
    {
        _sendGridSettings = sendGridSettings;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        var sendGridApiKey = EnvVariableHelper.GetByKey("SENDGRID_APIKEY");
        var client = new SendGridClient(sendGridApiKey);
        var from = new EmailAddress(_sendGridSettings.Value.FromEmail, _sendGridSettings.Value.FromName);
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, message, message);
        await client.SendEmailAsync(msg);
    }
}
