using Infrastructure.Configuration;
using Microsoft.AspNetCore.Cors.Infrastructure;
using CorsOptions = Infrastructure.Configuration.CorsOptions;

namespace Api.Extensions;


public static class CorsServiceCollectionExtensions
{
    /// <summary>
    /// Add CORS services with configuration from appsettings.json
    /// </summary>
    public static IServiceCollection AddCorsServices(this IServiceCollection services, IConfiguration configuration)
    {
        var corsOptions = configuration.GetSection(CorsOptions.SectionName).Get<CorsOptions>();

        if (corsOptions == null)
        {
            corsOptions = new CorsOptions();
        }

        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorsPolicy", policy =>
            {
                policy
                    .WithOrigins(corsOptions.AllowedOrigins)
                    .WithMethods(corsOptions.AllowedMethods)
                    .WithHeaders(corsOptions.AllowedHeaders)
                    .WithExposedHeaders(corsOptions.ExposedHeaders);

                if (corsOptions.AllowCredentials)
                {
                    policy.AllowCredentials();
                }

                policy.SetPreflightMaxAge(TimeSpan.FromSeconds(corsOptions.PreflightCacheDuration));
            });
        });

        // Register options for dependency injection
        services.Configure<CorsOptions>(configuration.GetSection(CorsOptions.SectionName));

        return services;
    }

    /// <summary>
    /// Add CORS services with default configuration (allows all for development)
    /// </summary>
    public static IServiceCollection AddCorsServicesDefault(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("DefaultCorsPolicy", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });

        return services;
    }
}