using Briefly.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;

namespace Briefly.Infrastructure.Persistence;

public static class Extensions
{
    private static readonly ILogger Logger = Log.ForContext(typeof(Extensions));

    internal static DbContextOptionsBuilder ConfigureDatabase(this DbContextOptionsBuilder builder,
        string connectionString)
    {
        builder.ConfigureWarnings(warnings => warnings.Log(RelationalEventId.PendingModelChangesWarning));
#if DEBUG

        builder.UseNpgsql(connectionString, e =>
            e.MigrationsAssembly("Briefly.Migrations")).EnableSensitiveDataLogging();
#endif

#if !DEBUG
        builder.UseNpgsql(connectionString, e =>
                    e.MigrationsAssembly("Briefly.Migrations"));
#endif


        return builder;
    }

    public static WebApplicationBuilder ConfigureDatabase(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.Services.AddOptions<DatabaseOptions>()
            .BindConfiguration(nameof(DatabaseOptions))
            .ValidateDataAnnotations()
            .PostConfigure(config =>
            {
                Logger.Information("Configuring database with connection string: {ConnectionString}",
                    config.ConnectionString);
            });

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditInterceptor>();

        return builder;
    }

    public static IServiceCollection BindDbContext<TContext>(this IServiceCollection services)
        where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddDbContext<TContext>((sp, options) =>
        {
            var dbConfig = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            options.ConfigureDatabase(dbConfig.ConnectionString);
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
        });


        return services;
    }
}