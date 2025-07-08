using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Domain.Events;
using System.Text.Json;

namespace Notes.Application.Notes.Create;

public class CreateNoteEndpoint(
    ILogger<CreateNoteEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)]
    IRepository<Note> repository,
    [FromKeyedServices(NotesMetadata.DIKey)]
    IRepository<NoteType> noteTypeRepository)
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
            NoteTypeId = req.NoteTypeId,
            Title = req.Title,
            CustomFields = JsonDocument.Parse(req.CustomFields.GetRawText())
        };

        note.QueueDomainEvent(new NoteCreated { Note = note });

        await repository.AddAsync(note, ct);
        await repository.SaveChangesAsync(ct);

        logger.LogInformation("Note created {NoteId}", note.Id);

        await SendAsync(note.Id, cancellation: ct);
    }
}