namespace Lsai.Api.Dtos;

public class QuestionDto
{
    public Guid Id { get; set; }

    public string Question { get; set; } = default!;

    public Guid DocumentationPartId { get; set; }
}
