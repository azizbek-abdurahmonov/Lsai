using Lsai.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lsai.Persistence.DbContexts;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public DbSet<UserCredentials> UserCredentials => Set<UserCredentials>();

    public DbSet<VerificationCode> VerificationCodes => Set<VerificationCode>();

    public DbSet<ResetPasswordVerificationCode> resetPasswordVerificationCodes => Set<ResetPasswordVerificationCode>();

    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
