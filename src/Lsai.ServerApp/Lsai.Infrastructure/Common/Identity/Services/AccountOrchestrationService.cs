using Lsai.Application.Common.Identity.Services;
using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Common.Enums;
using Lsai.Domain.Common.Exceptions;
using Lsai.Domain.Common.Identity;
using Lsai.Domain.Entities;
using Lsai.Persistence.Repositories.Interfaces;
using Lsai.Application.Common.Notification.Models;
using Lsai.Domain.Common.Constants;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace Lsai.Infrastructure.Common.Identity.Services;

public class AccountOrchestrationService(
    IUserService userService,
    IUserCredentialsRepository userCredentialsRepository,
    IPasswordHasherService passwordHasherService,
    IAccessTokenGeneratorService accessTokenGeneratorService,
    IVerificationCodeRepository verificationCodeRepository,
    IEmailSenderService emailSenderService,
    IResetPasswordVerificationCodeRepository resetPasswordVerificationCodeRepository)
    : IAccountOrchestrationService
{
    public async ValueTask<User> SignUpAsync(SignUpDetails signUpDetails, CancellationToken cancellationToken = default)
    {
        if (String.IsNullOrWhiteSpace(signUpDetails.Password) ||
            signUpDetails.Password.Length is < 8 or > 16)
        {
            throw new InvalidOperationException("Password length must be between 8 and 16");
        }

        var user = new User()
        {
            FirstName = signUpDetails.FirstName,
            LastName = signUpDetails.LastName,
            Email = signUpDetails.Email,
        };

        await userService.CreateAsync(user, cancellationToken: cancellationToken);

        var userCredentials = new UserCredentials()
        {
            PasswordHash = passwordHasherService.Hash(signUpDetails.Password),
            UserId = user.Id
        };

        await userCredentialsRepository.CreateAsync(userCredentials, cancellationToken: cancellationToken);
        await SendVerificationCodeAsync(user.Id, cancellationToken);

        return user;
    }

    public ValueTask<string> SignInAsync(SignInDetails signInDetails, CancellationToken cancellationToken = default)
    {
        var user = userService
            .Get(user => user.Email == signInDetails.Email)
            .FirstOrDefault() ?? throw new InvalidOperationException("User not found with this email!");

        var userCredentials = userCredentialsRepository
            .Get(userCredentials => userCredentials.UserId == user.Id)
            .FirstOrDefault() ?? throw new InvalidOperationException("User has no credentials");

        if (!passwordHasherService.Verify(signInDetails.Password, userCredentials.PasswordHash))
            throw new InvalidOperationException("Invalid password!");

        return new(accessTokenGeneratorService.GetToken(user));
    }

    public async ValueTask<bool> GrandRoleAsync(Guid userId, Role role, CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken)
            ?? throw new EntityNotFoundException(typeof(User));

        user.Role = role;

        await userService.UpdateAsync(user, cancellationToken: cancellationToken);

        return true;
    }

    public async ValueTask<bool> UpdatePasswordAsync(Guid userId, ChangePasswordDetails changePasswordDetails, CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken)
                   ?? throw new EntityNotFoundException(typeof(User));

        var userCredentials = userCredentialsRepository.Get(
            userCredentials => userCredentials.UserId == user.Id).FirstOrDefault()
            ?? throw new EntityNotFoundException(typeof(UserCredentials));

        if (!passwordHasherService.Verify(changePasswordDetails.OldPassword, userCredentials.PasswordHash))
            throw new InvalidOperationException("Old password is invalid");

        if (changePasswordDetails.NewPassword.Length is < 8 or > 16)
            throw new InvalidOperationException("Password length must be between 8 and 16");

        if (changePasswordDetails.NewPassword != changePasswordDetails.ConfirmPassword)
            throw new InvalidOperationException("new and confirm password is not equal");

        userCredentials.PasswordHash = passwordHasherService.Hash(changePasswordDetails.NewPassword);

        await userCredentialsRepository.UpdateAsync(userCredentials, cancellationToken: cancellationToken);

        return true;
    }

    public async ValueTask<bool> VerifyAccountAsync(Guid userId, int code, CancellationToken cancellationToken = default)
    {
        var verificationCode = verificationCodeRepository
            .Get(code => code.UserId == userId)
            .OrderBy(code => code.ExpirationTime)
            .LastOrDefault()
            ?? throw new EntityNotFoundException(typeof(VerificationCode));

        if (verificationCode.Code != code)
            throw new InvalidOperationException("Invalid code");

        if (verificationCode.ExpirationTime < DateTime.UtcNow)
            throw new InvalidOperationException("This code is expired");

        var user = await userService.GetByIdAsync(verificationCode.UserId, cancellationToken: cancellationToken)
            ?? throw new EntityNotFoundException(typeof(User));

        user.IsVerified = true;
        await userService.UpdateAsync(user, cancellationToken: cancellationToken);

        return true;
    }

    public async ValueTask<bool> SendVerificationCodeAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(userId)
            ?? throw new EntityNotFoundException(typeof(User));

        var random = new Random();
        var verificationCodeValue = random.Next(10000, 99999);

        var verificationCode = new VerificationCode
        {
            Code = verificationCodeValue,
            UserId = user.Id,
            ExpirationTime = DateTime.UtcNow.AddMinutes(30),
        };
        await verificationCodeRepository.CreateAsync(verificationCode, cancellationToken: cancellationToken);

        emailSenderService.SendEmail(
            new EmailMessage
            {
                ReceiverEmail = user.Email,
                Subject = NotificationConstants.VerificationCodeSubject,
                Body = NotificationConstants.VerificationCodeBody,
            },
            new Dictionary<string, string>()
            {
                { "{{code}}", $"{verificationCodeValue}" }
            }
        );

        return true;
    }

    public async ValueTask<bool> SendResetPasswordCodeAsync(Guid userId, string email, CancellationToken cancellationToken = default)
    {
        var user = await userService.GetByIdAsync(userId, cancellationToken: cancellationToken)
           ?? throw new EntityNotFoundException(typeof(User));

        if (user.Email != email)
            throw new InvalidOperationException("Invalid email");

        var random = new Random();
        var resetVerificationCodeValue = random.Next(10000, 99999);

        var resetPasswordVerificationCode = new ResetPasswordVerificationCode
        {
            UserId = userId,
            Code = resetVerificationCodeValue,
            ExpirationTime = DateTime.UtcNow.AddMinutes(30)
        };
        await resetPasswordVerificationCodeRepository.CreateAsync(resetPasswordVerificationCode, cancellationToken);

        emailSenderService.SendEmail(
            new EmailMessage
            {
                ReceiverEmail = user.Email,
                Subject = NotificationConstants.ResetPasswordCodeSubject,
                Body = NotificationConstants.ResetPasswordCodeBody,
            },
            new Dictionary<string, string>()
            {
                { "{{code}}", $"{resetVerificationCodeValue}" }
            }
        );

        return true;
    }

    public async ValueTask<bool> ResetPassword(Guid userId, ResetPasswordDetails resetPasswordDetails, CancellationToken cancellationToken = default)
    {
        var resetPasswordVerificationCode = await resetPasswordVerificationCodeRepository
            .Get(code => code.UserId == userId)
            .OrderBy(code => code.ExpirationTime)
            .LastOrDefaultAsync()
            ?? throw new EntityNotFoundException(typeof(ResetPasswordVerificationCode));

        if (resetPasswordVerificationCode.Code != resetPasswordDetails.Code)
            throw new InvalidOperationException("Invalid code!");

        if (resetPasswordVerificationCode.ExpirationTime < DateTime.UtcNow)
            throw new InvalidOperationException("This code is expired");

        if (resetPasswordDetails.Password.Length is < 8 or > 16)
            throw new InvalidOperationException("Password length must be beetwen 8 and 16");

        if (resetPasswordDetails.Password != resetPasswordDetails.ConfirmPassword)
            throw new InvalidOperationException("Password and confirm password is not equal");

        var user = await userService.GetByIdAsync(resetPasswordVerificationCode.UserId)
            ?? throw new EntityNotFoundException(typeof(User));

        var userCredentials = await userCredentialsRepository
            .Get(credentials => credentials.UserId == user.Id)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken)
            ?? throw new InvalidOperationException("User doesn't have credentials!");

        userCredentials.PasswordHash = passwordHasherService.Hash(resetPasswordDetails.Password);
        await userCredentialsRepository.UpdateAsync(userCredentials);

        return true;
    }
}
