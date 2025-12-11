using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        var useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

        Guard.Against.Null(useInMemoryDatabase, message: "Setting for 'UseInMemoryDatabase' does not exists.");

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

        if (useInMemoryDatabase)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("MemoryDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder =>
                    {
                        builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        builder.UseNodaTime();
                    }));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IClockService, ClockService>();

        return services;
    }
}
