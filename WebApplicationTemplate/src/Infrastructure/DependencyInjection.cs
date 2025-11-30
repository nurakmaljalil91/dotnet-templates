using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure services in the dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure services into the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="configuration">The application configuration.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // configure DbContext
        var defaultConnection = configuration.GetConnectionString("DefaultConnection");

        Guard.Against.Null(defaultConnection, message: "Connection string 'DefaultConnection' not found.");

        // Register health checks
        services
            .AddHealthChecks()
            .AddCheck("application", () => HealthCheckResult.Healthy())
            .AddNpgSql(
            connectionString: defaultConnection!,
            name: "postgres",
            tags: new[] { "ready"
            });

        return services;
    }
}
