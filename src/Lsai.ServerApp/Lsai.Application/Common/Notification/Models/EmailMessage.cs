namespace Lsai.Application.Common.Notification.Models;

public class EmailMessage
{
    public string ReceiverEmail { get; set; } = default!;

    public string Subject { get; set; } = default!; 

    public string Body { get; set; } = default!;

}
