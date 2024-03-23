using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Lsai.Api.Data.SeedData;

public static class SeedDataExtensions
{
    public static async ValueTask InitializeSeedData(this IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        if (!await dbContext.EmailTemplates.AnyAsync())
            await SeedEmailTemplatesAsync(dbContext, webHostEnvironment);

        if (!await dbContext.Users.AnyAsync())
            await SeedUsersAsync(dbContext, webHostEnvironment);

        if (!await dbContext.UserCredentials.AnyAsync())
            await SeedUserCredentialsAsync(dbContext, webHostEnvironment);

        if (dbContext.ChangeTracker.HasChanges())
            await dbContext.SaveChangesAsync();
    }

    private static async ValueTask SeedEmailTemplatesAsync(this AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
    {
        var webHost = webHostEnvironment.WebRootPath;
        var filePath = Path.Combine(webHost, "Data", "SeedData", "EmailTemplates.json");

        var emailTemplates = JsonSerializer.Deserialize<List<EmailTemplate>>(
            File.ReadAllText(filePath) ?? throw new InvalidOperationException());

        await dbContext.EmailTemplates.AddRangeAsync(emailTemplates!);
        await dbContext.SaveChangesAsync();
    }

    private static async ValueTask SeedUsersAsync(this AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
    {
        var webHost = webHostEnvironment.WebRootPath;
        var filePath = Path.Combine(webHost, "Data", "SeedData", "Users.json");

        var users = JsonSerializer.Deserialize<List<User>>(
            File.ReadAllText(filePath) ?? throw new InvalidOperationException());

        await dbContext.Users.AddRangeAsync(users ?? throw new InvalidOperationException());
        await dbContext.SaveChangesAsync();
    }

    private static async ValueTask SeedUserCredentialsAsync(this AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
    {
        var webHost = webHostEnvironment.WebRootPath;
        var filePath = Path.Combine(webHost, "Data", "SeedData", "UserCredentials.json");

        var userCredentials = JsonSerializer.Deserialize<List<UserCredentials>>(
            File.ReadAllText(filePath) ?? throw new InvalidOperationException());

        await dbContext.UserCredentials.AddRangeAsync(userCredentials ?? throw new InvalidOperationException());
        await dbContext.SaveChangesAsync();
    }
}
