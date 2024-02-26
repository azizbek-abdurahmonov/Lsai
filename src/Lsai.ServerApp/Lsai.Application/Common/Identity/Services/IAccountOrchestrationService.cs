using Lsai.Domain.Common.Enums;
using Lsai.Domain.Common.Identity;
using Lsai.Domain.Entities;

namespace Lsai.Application.Common.Identity.Services;

public interface IAccountOrchestrationService
{
    ValueTask<User> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default);

    ValueTask<string> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default);

    ValueTask<bool> GrandRoleAsync(Guid userId, Role role, CancellationToken cancellationToken = default);

    ValueTask<bool> UpdatePasswordAsync(Guid userId, ChangePasswordDetails changePasswordDetails, CancellationToken cancellationToken = default);

    ValueTask<bool> SendVerificationCodeAsync(Guid userId, CancellationToken cancellationToken = default);

    ValueTask<bool> VerifyAccountAsync(Guid userId, int code, CancellationToken cancellationToken = default);

    ValueTask<bool> SendResetPasswordCodeAsync(Guid userId, string email, CancellationToken cancellationToken = default);

    ValueTask<bool> ResetPassword(Guid userId, ResetPasswordDetails resetPasswordDetails, CancellationToken cancellationToken = default);
}
