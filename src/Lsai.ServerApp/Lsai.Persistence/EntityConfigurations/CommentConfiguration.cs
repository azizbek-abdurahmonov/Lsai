using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lsai.Persistence.EntityConfigurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasQueryFilter(comment => !comment.IsDeleted);

        builder.Property(comment => comment.Content).HasMaxLength(1024);

        builder
            .HasOne(comment => comment.DocumentationModel)
            .WithMany(documentation => documentation.Comments)
            .HasForeignKey(comment => comment.DocumentationId);

        builder
            .HasOne(comment => comment.User)
            .WithMany(user => user.Comments)
            .HasForeignKey(comment => comment.UserId);
    }
}
