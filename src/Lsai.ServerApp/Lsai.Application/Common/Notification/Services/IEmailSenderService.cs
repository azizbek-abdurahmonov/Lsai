using Lsai.Application.Common.Notification.Models;

namespace Lsai.Application.Common.Notification.Services;

public interface IEmailSenderService
{
    bool SendEmail(EmailMessage emailMessage, Dictionary<string, string> variables, bool isHtml = false);
}
