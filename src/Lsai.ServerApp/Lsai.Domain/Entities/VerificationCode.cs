using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class VerificationCode : AuditableEntity
{
    public int Code { get; set; }

    public Guid UserId { get; set; }

    public DateTime ExpirationTime { get; set; }
}
