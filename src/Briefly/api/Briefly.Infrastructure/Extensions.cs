using System.Text.Json;
using Briefly.Core.Persistence;
using Briefly.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace Briefly.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureBrieflyInfrastructure(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        //builder.Services.AddFastEndpoints(options =>
        //{
        //    options.Assemblies = [typeof(AppMetaData).Assembly];
        //});

        var pgConnectionString = builder.Configuration.GetConnectionString("briefly-platform-db");
        var cacheConnectionString = builder.Configuration.GetConnectionString("briefly-platform-cache");
        //builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection("DatabaseOptions"));

        if (string.IsNullOrEmpty(pgConnectionString))
            throw new Exception("PostgreSQL connection string is not configured.");
        //if (string.IsNullOrEmpty(cacheConnectionString))
        //    throw new Exception("Redis connection string is not configured.");


        builder.Services.Configure<DatabaseOptions>(options =>
        {
            builder.Configuration.GetSection("DatabaseOptions").Bind(options);
            options.ConnectionString = pgConnectionString; // Set the PostgreSQL connection string
        });

        // Configure Entity Framework Core with PostgreSQL
        // builder.Services.AddDbContext<NotesDbContext>(options =>
        //     options.UseNpgsql(pgConnectionString, b => b.MigrationsAssembly(typeof(MigrationsMetaData).Assembly.GetName().Name)));

        // Configure Entity Framework Core with DB Server
        builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = cacheConnectionString; });
        // Configure FusionCache with Redis
        builder.Services.AddFusionCache().AsHybridCache()
            .WithDefaultEntryOptions(new FusionCacheEntryOptions
            {
                Duration = TimeSpan.FromMinutes(5),
                IsFailSafeEnabled = true,
                FailSafeMaxDuration = TimeSpan.FromMinutes(10),
                FailSafeThrottleDuration = TimeSpan.FromSeconds(30),
                FactorySoftTimeout = TimeSpan.FromMilliseconds(100),
                FactoryHardTimeout = TimeSpan.FromMilliseconds(1500)
            })
            .WithSerializer(new FusionCacheNewtonsoftJsonSerializer())
            .WithDistributedCache(sp => sp.GetRequiredService<IDistributedCache>());

        // Add Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Briefly API",
                Version = "v1",
                Description = "API documentation for Briefly platform"
            });
        });

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        return builder;
    }

    public static async Task<WebApplication> UseBrieflyFramework(this WebApplication app)
    {
        app.UseFastEndpoints(c =>
        {
            c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            c.Endpoints.RoutePrefix = "api";
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) app.MapOpenApi();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        // Enable Swagger middleware
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Briefly API v1");
            c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
        });

        // Apply pending migrations automatically
        // using (var scope = app.Services.CreateScope())
        // {
        //     var dbContext = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
        //     dbContext.Database.Migrate();
        // }

        // Resolve and invoke IDbInitializer to apply migrations
        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.MigrateAsync(CancellationToken.None);
        }

        return app;
    }
}