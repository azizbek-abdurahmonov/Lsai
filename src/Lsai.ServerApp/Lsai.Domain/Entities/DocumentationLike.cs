using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class DocumentationLike : AuditableEntity
{
    public Guid UserId { get; set; }

    public Guid DocumentationId { get; set; }

    public virtual User? User { get; set; }

    public virtual DocumentationModel? Documentation { get; set; }
}
