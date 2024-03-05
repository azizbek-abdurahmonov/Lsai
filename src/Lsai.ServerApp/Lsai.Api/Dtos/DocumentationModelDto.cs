namespace Lsai.Api.Dtos;

public class DocumentationModelDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = default!;

    public string Technology { get; set; } = default!;

    public Guid UserId { get; set; }
}
