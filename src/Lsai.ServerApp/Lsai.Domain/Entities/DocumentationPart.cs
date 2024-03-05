using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class DocumentationPart : AuditableEntity
{
    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;

    public Guid DocumentationId { get; set; }

    public virtual DocumentationModel? Documentation { get; set; }
}
