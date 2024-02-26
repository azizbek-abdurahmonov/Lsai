namespace Lsai.Domain.Common.Identity;

public class ResetPasswordDetails
{
    public int Code { get; set; }

    public string Password { get; set; } = default!;

    public string ConfirmPassword { get; set; } = default!;
}
