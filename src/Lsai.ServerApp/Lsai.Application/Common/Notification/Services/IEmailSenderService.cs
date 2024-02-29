using Lsai.Application.Common.Notification.Models;

namespace Lsai.Application.Common.Notification.Services;

public interface IEmailSenderService
{
    bool SendEmail(EmailMessage emailMessage, bool isHtml = false);
}
