using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class FutureMail : AuditableEntity
{
    public string ReceiverEmail { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;

    public DateTime Date { get; set; }
}
