using Lsai.Domain.Entities;

namespace Lsai.Domain.Common.Notification;

public class EmailRequest
{
    public string ReceiverEmail { get; set; } = default!;

    public EmailTemplate EmailTemplate { get; set; } = default!;

    public Dictionary<string, string> Variables { get; set; } = new();
}
