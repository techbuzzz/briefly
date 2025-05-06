using Briefly.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Notes.Application.Domain;
using Shared.Constants;

namespace Notes.Application.Persistence;

public class NotesDbContext : DbContext
{
    public NotesDbContext(DbContextOptions<NotesDbContext> options, IOptions<DatabaseOptions> settings) : base(options)
    {
    }

    public DbSet<Note> Notes => Set<Note>();
    public DbSet<NoteType> NoteTypes => Set<NoteType>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotesDbContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Note);
    }
}