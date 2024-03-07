namespace Lsai.Api.Dtos;

public class QuestionOptionDto
{
    public Guid Id { get; set; }

    public string Answer { get; set; } = default!;

    public bool IsCorrect { get; set; }

    public Guid QuestionId { get; set; }
}
