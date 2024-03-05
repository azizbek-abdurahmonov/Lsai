namespace Lsai.Api.Dtos;

public class DocumentationPartDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public string Content { get; set; } = default!;

    public Guid DocumentationId { get; set; }
}
