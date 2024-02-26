using Lsai.Domain.Entities;

namespace Lsai.Application.Common.Identity.Services;

public interface IAccessTokenGeneratorService
{
    string GetToken(User user);
}
