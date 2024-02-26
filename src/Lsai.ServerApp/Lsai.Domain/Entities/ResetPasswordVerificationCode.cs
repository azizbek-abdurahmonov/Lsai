using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class ResetPasswordVerificationCode : AuditableEntity
{
    public int Code { get; set; }

    public Guid UserId { get; set; }

    public DateTime ExpirationTime { get; set; }
}
