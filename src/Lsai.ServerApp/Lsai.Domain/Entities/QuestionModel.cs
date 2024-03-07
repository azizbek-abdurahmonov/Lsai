using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class QuestionModel : AuditableEntity
{
    public string Question { get; set; } = default!;

    public Guid DocumentationPartId { get; set; }

    public virtual List<QuestionOption>? Options { get; set; }
}
