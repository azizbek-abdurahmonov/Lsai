using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lsai.Persistence.EntityConfigurations;

public class DocumentationLikeConfiguration : IEntityTypeConfiguration<DocumentationLike>
{
    public void Configure(EntityTypeBuilder<DocumentationLike> builder)
    {
        builder.HasQueryFilter(like => !like.IsDeleted);

        builder.HasOne(like => like.Documentation)
            .WithMany(documentation => documentation.Likes)
            .HasForeignKey(like => like.DocumentationId);

        builder.HasOne(like => like.User)
            .WithMany(user => user.Likes)
            .HasForeignKey(like => like.UserId);
    }
}
