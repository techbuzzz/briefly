using System.Text.Json;
using Briefly.Migrations;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Notes.Application;
using Notes.Infrastructure;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace Briefly.Infrastructure;

public static class Extensions
{
    public static WebApplicationBuilder ConfigureBrieflyInfrastructure(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.Services.AddFastEndpoints(options =>
        {
            options.Assemblies = [typeof(NotesApplicationModuleMetaData).Assembly];
        });

        var pgConnectionString = builder.Configuration.GetConnectionString("briefly-platform-db");
        var cacheConnectionString = builder.Configuration.GetConnectionString("briefly-platform-cache");

        if (string.IsNullOrEmpty(pgConnectionString))
            throw new Exception("PostgreSQL connection string is not configured.");
        //if (string.IsNullOrEmpty(cacheConnectionString))
        //    throw new Exception("Redis connection string is not configured.");

        // Configure Entity Framework Core with PostgreSQL
        builder.Services.AddDbContext<NotesDbContext>(options =>
            options.UseNpgsql(pgConnectionString, b => b.MigrationsAssembly(typeof(MigrationsMetaData).Assembly.GetName().Name)));

        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("vnt-platform-cache");
        });
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

    public static WebApplication UseBrieflyFramework(this WebApplication app)
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
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<NotesDbContext>();
            dbContext.Database.Migrate();
        }

        return app;
    }
}
