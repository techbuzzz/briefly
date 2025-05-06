using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.NotesTypes.Responses;
using Notes.Application.Persistence;

namespace Notes.Application.NotesTypes.Get;

public class GetNoteTypeEndpoint(
    ILogger<GetNoteTypeEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<NoteType> repository) 
    : Endpoint<GetNoteTypeRequest, GetNoteTypeResponse>
{
    public override void Configure()
    {
        Get("/note-types/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetNoteTypeRequest req, CancellationToken ct)
    {
        var noteType = await repository.GetByIdAsync(req.Id, ct);
        
        if (noteType is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetNoteTypeResponse
        {
            Id = noteType.Id,
            Name = noteType.Name,
            Description = noteType.Description,
            IsActive = noteType.IsActive
        };

        await SendAsync(response, cancellation: ct);
    }
}
