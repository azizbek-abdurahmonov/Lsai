using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lsai.Persistence.EntityConfigurations;

public class QuestionConfiguration : IEntityTypeConfiguration<QuestionModel>
{
    public void Configure(EntityTypeBuilder<QuestionModel> builder)
    {
        builder.Property(question => question.Question).HasMaxLength(256);
    }
}
