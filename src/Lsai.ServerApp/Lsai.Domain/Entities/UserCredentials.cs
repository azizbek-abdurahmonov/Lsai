using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class UserCredentials : AuditableEntity
{
    public string PasswordHash { get; set; } = default!;

    public Guid UserId { get; set; }

    public virtual User? User { get; set; }
}
