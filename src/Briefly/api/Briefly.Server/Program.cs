using Briefly.Infrastructure;
using Briefly.Infrastructure.Logging.Serilog;
using Serilog;

namespace Briefly.Server;

public class Program
{
    public static async Task Main(string[] args)
    {
        StaticLogger.EnsureInitialized();
        Log.Information("server booting up..");
        Log.Information("server args: {args}", string.Join(" ", args));
        Log.Information("server environment: {env}", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        Log.Information("server version: {version}", Environment.Version);
        Log.Information("server architecture: {arch}", Environment.Is64BitProcess ? "x64" : "x86");
        Log.Information("server framework: {framework}", Environment.Version.ToString());
        Log.Information("server os: {os}", Environment.OSVersion.ToString());

        try
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.ConfigureBrieflyInfrastructure();
            builder.RegisterModules();
            var app = builder.Build();

            await app.UseBrieflyFramework();
            app.UseModules();

            await app.RunAsync();
        }
        catch (Exception ex) when (!ex.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal))
        {
            StaticLogger.EnsureInitialized();
            Log.Fatal(ex.Message, "unhandled exception");
        }
        finally
        {
            StaticLogger.EnsureInitialized();
            Log.Information("server shutting down..");
            await Log.CloseAndFlushAsync();
        }
    }
}