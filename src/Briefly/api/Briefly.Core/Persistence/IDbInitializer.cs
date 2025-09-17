namespace Briefly.Core.Persistence;

// ...existing code...
// No changes needed here as the interface already defines MigrateAsync
// ...existing code...
public interface IDbInitializer
{
    Task MigrateAsync(CancellationToken cancellationToken);
    Task SeedAsync(CancellationToken cancellationToken);
}