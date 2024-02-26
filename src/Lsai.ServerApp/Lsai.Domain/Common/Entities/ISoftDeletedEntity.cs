namespace Lsai.Domain.Common.Entities;

public interface ISoftDeletedEntity : IEntity
{
    bool IsDeleted { get; set; }

    DateTime? DeletedTime { get; set; }
}
