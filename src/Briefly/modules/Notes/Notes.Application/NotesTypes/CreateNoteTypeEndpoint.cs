using FastEndpoints;
using Notes.Domain;
using Notes.Domain.Events;
using Notes.Infrastructure;

namespace Notes.Application.NotesTypes;

public class CreateNoteTypeEndpoint : Endpoint<CreateNoteTypeRequest, Guid>
{
    private readonly NotesDbContext _db;

    public CreateNoteTypeEndpoint(NotesDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Post("/note-types");
        AllowAnonymous();
        //Group<NotesGroup>();
    }

    public override async Task HandleAsync(CreateNoteTypeRequest req, CancellationToken ct)
    {
        var entity = new NoteType
        {
            Id = Guid.NewGuid(),
            Name = req.Name,
            Description = req.Description
           
        };
        entity.QueueDomainEvent(new NoteTypeCreated { NoteType = entity });

        _db.NoteTypes.Add(entity);
        await _db.SaveChangesAsync(ct);

        await SendAsync(entity.Id, cancellation: ct);
    }
}