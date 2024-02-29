using FluentValidation;
using Lsai.Api.CustomMiddlewares;
using Lsai.Api.Data.SeedData;
using Lsai.Application.Common.Identity.Services;
using Lsai.Application.Common.Notification.Services;
using Lsai.Domain.Common.Constants;
using Lsai.Infrastructure.Common.Identity.Services;
using Lsai.Infrastructure.Common.Notification.Services;
using Lsai.Infrastructure.Common.Settings;
using Lsai.Persistence.DbContexts;
using Lsai.Persistence.Interceptors;
using Lsai.Persistence.Repositories;
using Lsai.Persistence.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Lsai.Api.Configuration;

public static partial class HostConfiguration
{
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(SwaggerConstants.SecurityDefinitionName, new OpenApiSecurityScheme
            {
                Name = SwaggerConstants.SecuritySchemeName,
                Type = SecuritySchemeType.ApiKey,
                Scheme = SwaggerConstants.SecurityScheme,
                In = ParameterLocation.Header,
                Description = SwaggerConstants.SwaggerAuthorizationDescription
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SwaggerConstants.SecurityScheme
                        }
                    },
                Array.Empty<string>()
                }
            });
        });

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddRouting(options => options.LowercaseUrls = true);

        return builder;
    }

    private static WebApplicationBuilder AddIdentityInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(nameof(JwtSettings)));

        builder.Services
            .AddSingleton<IPasswordHasherService, PasswordHasherService>()
            .AddSingleton<IAccessTokenGeneratorService, AccessTokenGeneratorService>();

        builder.Services
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();

        builder.Services
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAccountOrchestrationService, AccountOrchestrationService>();

        var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>()
            ?? throw new InvalidOperationException("JwtSettings is not configured!");

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = jwtSettings.ValidateIssuer,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    ValidateAudience = jwtSettings.ValidateAudience,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidateLifetime = jwtSettings.ValidateLifeTime,
                    ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            }
        );

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<UpdateAuditableInterceptor>();

        builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"));
            options.AddInterceptors(serviceProvider.GetRequiredService<UpdateAuditableInterceptor>());
        });

        return builder;
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        assemblies.Add(Assembly.GetExecutingAssembly());

        builder.Services.AddValidatorsFromAssemblies(assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder builder)
    {
        var assemblies = Assembly.GetExecutingAssembly()
                                              .GetReferencedAssemblies().Select(Assembly.Load)
                                              .ToList();

        assemblies.Add(Assembly.GetExecutingAssembly());

        builder.Services.AddAutoMapper(assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddNotificationInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection(nameof(SmtpSettings)));

        builder.Services
            .AddScoped<IResetPasswordVerificationCodeRepository, ResetPasswordVerificationCodeRepository>()
            .AddScoped<IVerificationCodeRepository, VerificationCodeRepository>()
            .AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();

        builder.Services
            .AddScoped<IEmailSenderService, EmailSenderService>()
            .AddScoped<IEmailOrchestrationService, EmailOrchestrationService>()
            .AddScoped<IEmailTemplateService, EmailTemplateService>()
            .AddScoped<IEmailRenderService, EmailRenderService>();

        return builder;
    }

    private static WebApplicationBuilder ApplyMigrations(this WebApplicationBuilder builder)
    {
        using var scope = builder.Services.BuildServiceProvider().CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();

        return builder;
    }

    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options => options.AddPolicy("AllowSpecificOrigin", policy =>
        {
            policy
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        }));

        return builder;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app
            .UseSwagger()
            .UseSwaggerUI();

        return app;
    }

    private static WebApplication UseCustomMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }

    private static async Task<WebApplication> InitializeSeedData(this WebApplication app)
    {
        var serviceProvider = app.Services.CreateScope().ServiceProvider;

        await serviceProvider.InitializeSeedData();

        return app;
    }
}