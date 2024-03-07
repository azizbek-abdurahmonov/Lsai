using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lsai.Persistence.EntityConfigurations;

public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
{
    public void Configure(EntityTypeBuilder<QuestionOption> builder)
    {
        builder
            .HasOne(option => option.Question)
            .WithMany(question => question.Options)
            .HasForeignKey(option => option.QuestionId);
    }
}
