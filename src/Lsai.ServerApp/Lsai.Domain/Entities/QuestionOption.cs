using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class QuestionOption : AuditableEntity
{
    public string Answer { get; set; } = default!;

    public bool IsCorrect { get; set; }

    public Guid QuestionId { get; set; }

    public virtual QuestionModel? Question { get; set; }
}
