using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using Notes.Infrastructure.Persistence;
using Shared.Constants;

namespace Notes.Infrastructure
{
    public class NotesDbContext : DbContext
    {
        public NotesDbContext(DbContextOptions<NotesDbContext> options) : base(options) { }
        public DbSet<Note> Notes => Set<Note>();
        public DbSet<NoteType> NoteTypes => Set<NoteType>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ArgumentNullException.ThrowIfNull(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(NotesDbContext).Assembly);
            modelBuilder.HasDefaultSchema(SchemaNames.Note);
        }
    }
}
