using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lsai.Persistence.EntityConfigurations;

public class DocumentationConfiguration : IEntityTypeConfiguration<DocumentationModel>
{
    public void Configure(EntityTypeBuilder<DocumentationModel> builder)
    {
        builder.HasIndex(documentation => documentation.Technology).IsUnique();

        builder.HasQueryFilter(documentation => !documentation.IsDeleted);

        builder.Property(documentation => documentation.Title).HasMaxLength(512);
        builder.Property(documentation => documentation.Technology).HasMaxLength(256);

        builder
            .HasOne(documentation => documentation.User)
            .WithMany(user => user.Documentations)
            .HasForeignKey(documentation => documentation.UserId);
    }
}
