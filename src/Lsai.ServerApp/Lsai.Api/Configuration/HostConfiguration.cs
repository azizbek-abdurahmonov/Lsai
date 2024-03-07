namespace Lsai.Api.Configuration;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddDevTools()
            .AddCors()
            .AddIdentityInfrastructure()
            .AddNotificationInfrastructure()
            .AddDocumentationInfrastructure()
            .AddQAInfrastructure()
            .AddValidators()
            .AddAutoMapper()
            .AddPersistence()
            .AddExposers()
            .ApplyMigrations();


        return new(builder);
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .UseExposers()
            .UseCustomMiddlewares();

        await app.InitializeSeedData();

        return app;
    }
}
