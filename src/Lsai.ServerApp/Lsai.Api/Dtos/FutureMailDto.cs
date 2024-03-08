namespace Lsai.Api.Dtos;

public class FutureMailDto
{
    public string ReceiverEmail { get; set; } = default!;

    public string Subject { get; set; } = default!;

    public string Body { get; set; } = default!;

    public DateTime Date { get; set; }
}
