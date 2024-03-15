using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lsai.Persistence.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<UserCredentials> UserCredentials => Set<UserCredentials>();

    public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();

    public DbSet<ResetPasswordVerificationCode> ResetPasswordVerificationCodes => Set<ResetPasswordVerificationCode>();

    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();

    public DbSet<DocumentationModel> Documentations => Set<DocumentationModel>();

    public DbSet<DocumentationPart> DocumentationParts => Set<DocumentationPart>();

    public DbSet<QuestionModel> Questions => Set<QuestionModel>();

    public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();

    public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();

    public DbSet<FutureMail> FutureMails => Set<FutureMail>();  

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
