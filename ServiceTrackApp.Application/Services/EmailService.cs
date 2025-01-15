using ServiceTrackApp.Application.Interfaces.Email;

namespace ServiceTrackApp.Application.Services;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
        throw new NotImplementedException();
    }
}