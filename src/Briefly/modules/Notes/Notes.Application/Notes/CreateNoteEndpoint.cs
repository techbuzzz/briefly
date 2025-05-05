using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Domain.Events;
using Notes.Application.Persistence;

namespace Notes.Application.Notes;

public class CreateNoteEndpoint(
    ILogger<CreateNoteEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<Note> repository,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<NoteType> noteTypeRepository) 
    : Endpoint<CreateNoteRequest, Guid>
{
    public override void Configure()
    {
        Post("/notes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateNoteRequest req, CancellationToken ct)
    {
        // Verify NoteType exists
        var noteType = await noteTypeRepository.GetByIdAsync(req.NoteTypeId, ct);
        if (noteType is null)
        {
            AddError("NoteTypeId", "Note type not found");
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var note = new Note
        {
            Id = Guid.NewGuid(),
            AuthorName = req.AuthorName,
            AuthorEmail = req.AuthorEmail,
            NoteTypeId = req.NoteTypeId,
            Date = req.Date,
            Mood = req.Mood,
            Energy = req.Energy,
            Feeling = req.Feeling,
            Summary = req.Summary,
            HtmlContent = req.HtmlContent,
            RawData = req.RawData,
            CreatedAt = DateTime.UtcNow
        };
        
        note.QueueDomainEvent(new NoteCreated { Note = note });

        await repository.AddAsync(note, ct);
        await repository.SaveChangesAsync(ct);
        
        logger.LogInformation("Note created {NoteId}", note.Id);

        await SendAsync(note.Id, cancellation: ct);
    }
}
