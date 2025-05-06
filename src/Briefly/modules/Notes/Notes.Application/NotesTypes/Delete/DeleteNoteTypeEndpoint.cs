using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Domain.Events;
using Notes.Application.Persistence;

namespace Notes.Application.NotesTypes.Delete;

public class DeleteNoteTypeEndpoint(
    ILogger<DeleteNoteTypeEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<NoteType> repository) 
    : Endpoint<DeleteNoteTypeRequest, Guid>
{
    public override void Configure()
    {
        Delete("/note-types/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteNoteTypeRequest req, CancellationToken ct)
    {
        var noteType = await repository.GetByIdAsync(req.Id, ct);
        
        if (noteType is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }
        
        // Queue domain event before deletion
        noteType.QueueDomainEvent(new NoteTypeDeleted 
        { 
            NoteTypeId = noteType.Id,
            NoteTypeName = noteType.Name
        });
        
        // Delete entity
        await repository.DeleteAsync(noteType, ct);
        await repository.SaveChangesAsync(ct);
        
        logger.LogInformation("Note type deleted {NoteTypeId}", req.Id);
        
        await SendAsync(req.Id, cancellation: ct);
    }
}
