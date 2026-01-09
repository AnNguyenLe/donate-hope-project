using System.Net.Mail;

namespace DonateHope.Core.ServiceContracts.Email;

public interface ISmtpClientProvider : IDisposable
{
    Task SendMailAsync(string recipientEmail, MailMessage mailMessage);
}
