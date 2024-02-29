using Lsai.Domain.Common.Enums;
using Lsai.Domain.Entities;

namespace Lsai.Domain.Common.Notification;

public class EmailRequest
{
    public string ReceiverEmail { get; set; } = default!;

    public NotificationType NotificationType { get; set; }

    public EmailTemplate? EmailTemplate { get; set; }

    public Dictionary<string, string> Variables { get; set; } = new();
}
