namespace finebe.core.services.Messaging;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
}
