using System.Text.Json;
using Briefly.Core.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;

namespace Notes.Application.Persistence;

public sealed class NotesDbInitializer(ILogger<NotesDbInitializer> logger, NotesDbContext context) : IDbInitializer
{
   public async Task MigrateAsync(CancellationToken cancellationToken)
   {
      var appliedMigrations = await context.Database.GetAppliedMigrationsAsync(cancellationToken).ConfigureAwait(false);
      var allMigrations = await context.Database.GetPendingMigrationsAsync(cancellationToken).ConfigureAwait(false);
      if (allMigrations.Any())
      {
         await context.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
         logger.LogInformation("Applied database migrations for Note module");
      }
   }

   public async Task SeedAsync(CancellationToken cancellationToken)
   {
      // Seed NoteTypes if none exist
      if (!await context.NoteTypes.AnyAsync(cancellationToken))
      {
         var noteType = new NoteType
         {
            Id = Guid.NewGuid(),
            Name = "Journal",
            Description = "Personal journal entries",
            IsActive = true,
            FieldDefinitions =
            [
               new NoteFieldDefinition
               {
                  Id = Guid.NewGuid(),
                  FieldKey = "mood",
                  Label = "Mood",
                  DataType = FieldDataType.Choice,
                  IsRequired = false,
                  Order = 1,
                  Options =
                  [
                     new NoteFieldOption { Id = Guid.NewGuid(), Key = "happy", Value = "Happy" },
                     new NoteFieldOption { Id = Guid.NewGuid(), Key = "sad", Value = "Sad" },
                     new NoteFieldOption { Id = Guid.NewGuid(), Key = "neutral", Value = "Neutral" }
                  ]
               },
               new NoteFieldDefinition
               {
                  Id = Guid.NewGuid(),
                  FieldKey = "summary",
                  Label = "Summary",
                  DataType = FieldDataType.Text,
                  IsRequired = false,
                  Order = 2
               }
            ]
         };
         context.NoteTypes.Add(noteType);
         await context.SaveChangesAsync(cancellationToken);
         logger.LogInformation("Seeded initial NoteType and related field definitions/options");

         // Seed example notes if none exist
         if (!await context.Notes.AnyAsync(cancellationToken))
         {
            var notes = new List<Note>
            {
               new()
               {
                  Id = Guid.NewGuid(),
                  NoteTypeId = noteType.Id,
                  Title = "A Happy Day",
                  CustomFields = JsonDocument.Parse("{\"mood\":\"happy\",\"summary\":\"Had a great day!\"}")
               },
               new()
               {
                  Id = Guid.NewGuid(),
                  NoteTypeId = noteType.Id,
                  Title = "A Tough Day",
                  CustomFields = JsonDocument.Parse("{\"mood\":\"sad\",\"summary\":\"It was a tough day.\"}")
               },
               new()
               {
                  Id = Guid.NewGuid(),
                  NoteTypeId = noteType.Id,
                  Title = "Just Another Day",
                  CustomFields = JsonDocument.Parse("{\"mood\":\"neutral\",\"summary\":\"Just an average day.\"}")
               }
            };
            context.Notes.AddRange(notes);
            await context.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Seeded example notes");
         }
      }
   }
}