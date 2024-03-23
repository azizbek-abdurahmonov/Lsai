namespace Lsai.Api.Dtos;

public class CommentDto
{
    public Guid Id { get; set; }

    public string Content { get; set; } = default!;

    public Guid? ParentId { get; set; }

    public Guid UserId { get; set; }

    public Guid DocumentationId { get; set; }
}
