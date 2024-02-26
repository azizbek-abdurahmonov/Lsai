using Lsai.Domain.Common.Enums;

namespace Lsai.Api.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public Role Role { get; set; } = Role.User;
}
