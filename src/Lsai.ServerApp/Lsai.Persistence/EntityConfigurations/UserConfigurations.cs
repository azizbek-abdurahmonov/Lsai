using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lsai.Persistence.EntityConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasQueryFilter(user => !user.IsDeleted);

        builder.Property(user => user.FirstName).IsRequired().HasMaxLength(128);

        builder.Property(user => user.LastName).IsRequired().HasMaxLength(128);

        builder.Property(user => user.Email).IsRequired();

        builder
            .HasOne(user => user.Credentials)
            .WithOne(userCredentials => userCredentials.User)
            .HasForeignKey<UserCredentials>(userCredentials => userCredentials.UserId);
    }
}
