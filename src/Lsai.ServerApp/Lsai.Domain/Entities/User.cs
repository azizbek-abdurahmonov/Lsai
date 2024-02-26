﻿using Lsai.Domain.Common.Entities;
using Lsai.Domain.Common.Enums;

namespace Lsai.Domain.Entities;

public class User : AuditableEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public bool IsVerified { get; set; }

    public Role Role { get; set; } = Role.User;

    public virtual UserCredentials? Credentials { get; set; }
}