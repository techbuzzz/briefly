using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Domain.Events;

namespace Notes.Application.Notes.Update;

public class UpdateNoteEndpoint(
    ILogger<UpdateNoteEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)]
    IRepository<Note> repository,
    [FromKeyedServices(NotesMetadata.DIKey)]
    IRepository<NoteType> noteTypeRepository)
    : Endpoint<UpdateNoteRequest, Guid>
{
    public override void Configure()
    {
        Put("/notes/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateNoteRequest req, CancellationToken ct)
    {
        // Verify NoteType exists
        var noteType = await noteTypeRepository.GetByIdAsync(req.NoteTypeId, ct);
        if (noteType is null)
        {
            AddError("NoteTypeId", "Note type not found");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var note = await repository.GetByIdAsync(req.Id, ct);
        if (note is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Update properties
        note.AuthorName = req.AuthorName;
        note.AuthorEmail = req.AuthorEmail;
        note.NoteTypeId = req.NoteTypeId;
        note.Date = req.Date;
        note.Mood = req.Mood;
        note.Energy = req.Energy;
        note.Feeling = req.Feeling;
        note.Summary = req.Summary;
        note.HtmlContent = req.HtmlContent;
        note.RawData = req.RawData;

        // Queue domain event
        note.QueueDomainEvent(new NoteUpdated { Note = note });

        // Save changes
        await repository.UpdateAsync(note, ct);
        await repository.SaveChangesAsync(ct);

        logger.LogInformation("Note updated {NoteId}", note.Id);

        await SendAsync(note.Id, cancellation: ct);
    }
}