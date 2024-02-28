using Lsai.Domain.Common.Entities;
using Lsai.Domain.Common.Enums;

namespace Lsai.Domain.Entities;

public class EmailTemplate : AuditableEntity
{
    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public NotificationType Type { get; set; }
}
