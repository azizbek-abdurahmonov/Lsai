using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lsai.Persistence.EntityConfigurations;

public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
{
    public void Configure(EntityTypeBuilder<UserAnswer> builder)
    {
        builder
            .HasOne(answer => answer.User)
            .WithMany(user => user.Answers)
            .HasForeignKey(answer => answer.UserId);

        builder
            .HasOne(answer => answer.Question)
            .WithMany()
            .HasForeignKey(answer => answer.QuestionId);
    }
}
    