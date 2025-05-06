using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Domain.Events;

namespace Notes.Application.NotesTypes.Create;

public class CreateNoteTypeEndpoint(
    ILogger<CreateNoteTypeEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)]
    IRepository<NoteType> repository) : Endpoint<CreateNoteTypeRequest, Guid>
{
    // private readonly NotesDbContext _db;

    // public CreateNoteTypeEndpoint(IRepository<NoteType> repository)
    // {

    // _db = db;
    // }

    public override void Configure()
    {
        Post("/note-types");
        AllowAnonymous();
        //Group<NotesGroup>();
    }

    public override async Task HandleAsync(CreateNoteTypeRequest req, CancellationToken ct)
    {
        var item = new NoteType
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            Description = req.Description
        };
        item.QueueDomainEvent(new NoteTypeCreated { NoteType = item });

        await repository.AddAsync(item, ct).ConfigureAwait(false);
        await repository.SaveChangesAsync(ct).ConfigureAwait(false);
        logger.LogInformation("note type item created {NoteTypeItemId}", item.Id);

        await SendAsync(item.Id, cancellation: ct);
        // return new CreateTodoResponse(item.Id);
    }
}