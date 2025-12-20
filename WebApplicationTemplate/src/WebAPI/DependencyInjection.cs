using Application.Common.Interfaces;
using NSwag;
using NSwag.Generation.Processors.Security;
using WebAPI.Middlewares;
using WebAPI.Services;

namespace WebAPI;

/// <summary>
/// Provides extension methods for registering WebAPI services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers WebAPI services into the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddWebAPIServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register HttpContextAccessor
        services.AddHttpContextAccessor();

        services.AddScoped<IUser, CurrentUser>();

        // Allow all origin since it will be web service
        services.AddCors(options =>
        {
            options.AddPolicy(
                name: "CorsPolicy",
                policy =>
                {
                    policy
                        .SetIsOriginAllowed(origin => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // Required for SignalR
                });
        });

        // Register OpenAPI document generation
        services.AddOpenApiDocument(options =>
        {
            options.PostProcess = document =>
            {
                document.Info = new OpenApiInfo
                {
                    Title = "Web System",
                    Version = "v1",
                    Description = "System Web API",
                    TermsOfService = "https://example.com/terms",
                    Contact = new OpenApiContact
                    {
                        Name = "Nur Akmal Bin Jalil",
                        Email = "nurakmaljalil91@gmail.com",
                        Url = "https://nurakmaljalil.com/"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Example License",
                        Url = "https://example.com/license"
                    }
                };
            };

            // Add JWT token authorization
            options.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "Authorization",
                In = OpenApiSecurityApiKeyLocation.Header,
                Description = "Type into the text box: Bearer {your JWT token}."
            });

            options.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
        });

        // TODO: Configure Authentication

        // TODO: Configure Authorization

        services.AddTransient<ExceptionHandlingMiddleware>();

        return services;
    }
}
