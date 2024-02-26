namespace Lsai.Domain.Common.Identity;

public class ChangePasswordDetails
{
    public string OldPassword { get; set; } = default!;

    public string NewPassword { get; set; } = default!;

    public string ConfirmPassword { get; set;} = default!;
}
