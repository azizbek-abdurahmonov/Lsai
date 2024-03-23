using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class Comment : AuditableEntity
{
    public string Content { get; set; } = default!;

    public Guid? ParentId { get; set; }

    public Guid DocumentationId { get; set; }

    public Guid UserId { get; set; }

    public virtual User? User { get; set; }

    public virtual DocumentationModel? DocumentationModel { get; set; }
}
