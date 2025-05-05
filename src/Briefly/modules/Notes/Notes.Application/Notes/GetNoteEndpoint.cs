using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Persistence;

namespace Notes.Application.Notes;

public class GetNoteEndpoint(
    ILogger<GetNoteEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<Note> repository)
    : Endpoint<GetNoteRequest, GetNoteResponse>
{
    public override void Configure()
    {
        Get("/notes/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetNoteRequest req, CancellationToken ct)
    {
        var note = await repository.Query()
            .Include(n => n.NoteType)
            .FirstOrDefaultAsync(n => n.Id == req.Id, ct);
        
        if (note is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new GetNoteResponse
        {
            Id = note.Id,
            AuthorName = note.AuthorName,
            AuthorEmail = note.AuthorEmail,
            NoteTypeId = note.NoteTypeId,
            NoteTypeName = note.NoteType.Name,
            Date = note.Date,
            Mood = note.Mood,
            Energy = note.Energy,
            Feeling = note.Feeling,
            Summary = note.Summary,
            HtmlContent = note.HtmlContent,
            RawData = note.RawData,
            CreatedAt = note.CreatedAt
        };

        await SendAsync(response, cancellation: ct);
    }
}
