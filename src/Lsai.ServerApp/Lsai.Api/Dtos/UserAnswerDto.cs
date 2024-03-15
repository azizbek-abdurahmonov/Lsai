namespace Lsai.Api.Dtos;

public class UserAnswerDto
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid QuestionId { get; set; }

    public Guid OptionId { get; set; }
}
