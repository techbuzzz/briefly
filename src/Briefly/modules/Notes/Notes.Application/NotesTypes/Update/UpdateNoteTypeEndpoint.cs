using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Domain.Events;
using Notes.Application.Persistence;

namespace Notes.Application.NotesTypes.Update;

public class UpdateNoteTypeEndpoint(
    ILogger<UpdateNoteTypeEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<NoteType> repository) 
    : Endpoint<UpdateNoteTypeRequest, Guid>
{
    public override void Configure()
    {
        Put("/note-types/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateNoteTypeRequest req, CancellationToken ct)
    {
        var noteType = await repository.GetByIdAsync(req.Id, ct);
        
        if (noteType is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Update properties
        noteType.Name = req.Name;
        noteType.Description = req.Description;
        noteType.IsActive = req.IsActive;
        
        // Queue domain event
        noteType.QueueDomainEvent(new NoteTypeUpdated { NoteType = noteType });
        
        // Save changes
        await repository.UpdateAsync(noteType, ct);
        await repository.SaveChangesAsync(ct);
        
        logger.LogInformation("Note type updated {NoteTypeId}", noteType.Id);
        
        await SendAsync(noteType.Id, cancellation: ct);
    }
}
