using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Domain.Events;

namespace Notes.Application.Notes.Delete;

public class DeleteNoteEndpoint(
    ILogger<DeleteNoteEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)]
    IRepository<Note> repository)
    : Endpoint<DeleteNoteRequest, Guid>
{
    public override void Configure()
    {
        Delete("/notes/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteNoteRequest req, CancellationToken ct)
    {
        var note = await repository.GetByIdAsync(req.Id, ct);

        if (note is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Queue domain event before deletion
        note.QueueDomainEvent(new NoteDeleted
        {
            NoteId = note.Id,
            AuthorName = note.AuthorName
        });

        // Delete entity
        await repository.DeleteAsync(note, ct);
        await repository.SaveChangesAsync(ct);

        logger.LogInformation("Note deleted {NoteId}", req.Id);

        await SendAsync(req.Id, cancellation: ct);
    }
}