using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class DocumentationModel : AuditableEntity
{
    public string Title { get; set; } = default!;

    public string Technology { get; set; } = default!;

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public virtual List<DocumentationPart> Parts { get; set; } = new();
}
