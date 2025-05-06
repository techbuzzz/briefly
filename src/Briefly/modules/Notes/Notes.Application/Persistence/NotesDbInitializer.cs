using Briefly.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Notes.Application.Persistence;

public sealed class NotesDbInitializer(ILogger<NotesDbInitializer> logger, NotesDbContext context) : IDbInitializer
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await context.Database.GetPendingMigrationsAsync(cancellationToken).ConfigureAwait(false)).Any())
            await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
    }
}