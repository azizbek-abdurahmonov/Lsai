using Lsai.Application.Common.Notification.Models;
using Lsai.Application.Common.Notification.Services;
using Lsai.Infrastructure.Common.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Lsai.Infrastructure.Common.Notification.Services;

public class EmailSenderService(IOptions<SmtpSettings> smtpSettings) : IEmailSenderService
{
    public bool SendEmail(EmailMessage emailMessage, bool isHtml = false)
    {
        try
        {
            var senderEmail = smtpSettings.Value.Email;
            var senderPassword = smtpSettings.Value.Password;

            var mail = new MailMessage(senderEmail, emailMessage.ReceiverEmail);
            mail.Subject = emailMessage.Subject;
            mail.Body = emailMessage.Body;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
            smtpClient.EnableSsl = true;

            if (isHtml)
                mail.IsBodyHtml = true;

            smtpClient.Send(mail);
            return true;
                
        }
        catch
        {
            return false;
        }
    }
}
