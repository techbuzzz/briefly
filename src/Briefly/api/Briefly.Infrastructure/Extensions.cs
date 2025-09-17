using Briefly.Core.Persistence;
using Briefly.Infrastructure.Cors;
using Briefly.Infrastructure.Persistence;
using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.NewtonsoftJson;

namespace Briefly.Infrastructure;

public static class Extensions
{
   public static WebApplicationBuilder ConfigureBrieflyInfrastructure(this WebApplicationBuilder builder)
   {
      ArgumentNullException.ThrowIfNull(builder);
      
      var pgConnectionString = builder.Configuration.GetConnectionString("briefly-platform-db");
      var cacheConnectionString = builder.Configuration.GetConnectionString("briefly-platform-cache");

      if (string.IsNullOrEmpty(pgConnectionString))
         throw new Exception("PostgreSQL connection string is not configured.");

      builder.Services.Configure<DatabaseOptions>(options =>
      {
         builder.Configuration.GetSection("DatabaseOptions").Bind(options);
         options.ConnectionString = pgConnectionString; // Set the PostgreSQL connection string
      });

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
      builder.ConfigureDatabase();

      builder.Services.AddCorsPolicy(builder.Configuration);

      return builder;
   }

   public static async Task<WebApplication> UseBrieflyFramework(this WebApplication app)
   {
      ArgumentNullException.ThrowIfNull(app);

      app.UseFastEndpoints(c =>
      {
         c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
         c.Endpoints.RoutePrefix = "api";
      });

      app.UseHttpsRedirection();

      app.UseCorsPolicy();

      app.UseAuthorization();

      // Enable Swagger middleware
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
         c.SwaggerEndpoint("/swagger/v1/swagger.json", "Briefly API v1");
         c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
         // c.RoutePrefix = "swagger";
      });


      app.SetupDatabases();

      return app;
   }

   private static void SetupDatabases(this IApplicationBuilder app)
   {
      using var scope = app.ApplicationServices.CreateScope();

      var initializers = scope.ServiceProvider.GetServices<IDbInitializer>();
      foreach (var initializer in initializers)
      {
         initializer.MigrateAsync(CancellationToken.None).Wait();
         initializer.SeedAsync(CancellationToken.None).Wait();
      }
   }
}