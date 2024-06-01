namespace finebe.webapi.Src.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string message);
}