namespace Lsai.Api.Configuration;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddDevTools()
            .AddIdentityInfrastructure()
            .AddNotificationInfrastructure()
            .AddValidators()
            .AddAutoMapper()
            .AddPersistence()
            .AddExposers();

        return new(builder);
    }

    public static ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        app
            .UseDevTools()
            .UseExposers()
            .UseCustomMiddlewares();

        return new(app);
    }
}
