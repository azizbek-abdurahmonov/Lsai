using Lsai.Application.Common.Identity.Services;
using BC = BCrypt.Net.BCrypt;

namespace Lsai.Infrastructure.Common.Identity.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string Hash(string password)
        => BC.HashPassword(password);

    public bool Verify(string password, string hash)
        => BC.Verify(password, hash);
}
