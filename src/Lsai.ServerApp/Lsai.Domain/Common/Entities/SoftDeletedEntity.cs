namespace Lsai.Domain.Common.Entities;

public class SoftDeletedEntity : Entity, ISoftDeletedEntity
{
    public bool IsDeleted { get; set; }

    public DateTime? DeletedTime { get; set; }
}
