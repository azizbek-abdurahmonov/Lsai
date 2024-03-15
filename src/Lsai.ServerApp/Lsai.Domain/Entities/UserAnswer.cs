using Lsai.Domain.Common.Entities;

namespace Lsai.Domain.Entities;

public class UserAnswer : AuditableEntity
{
    public Guid UserId { get; set; }
        
    public Guid QuestionId { get; set; }

    public Guid OptionId { get; set; }

    public virtual User? User { get; set; }

    public virtual QuestionModel? Question { get; set; }
}
