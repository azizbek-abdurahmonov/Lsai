using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lsai.Persistence.EntityConfigurations;

public class DocumentationPartConfiguration : IEntityTypeConfiguration<DocumentationPart>
{
    public void Configure(EntityTypeBuilder<DocumentationPart> builder)
    {
        builder.Property(documentationPart => documentationPart.Title).HasMaxLength(256);

        builder
            .HasOne(docPart => docPart.Documentation)
            .WithMany(documentation => documentation.Parts)
            .HasForeignKey(documentationPart => documentationPart.DocumentationId);
    }
}
