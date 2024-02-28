using Lsai.Domain.Entities;
using Lsai.Persistence.DbContexts;
using Microsoft.AspNetCore.Hosting;
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
}
