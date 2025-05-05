using Ardalis.Specification;
using Briefly.Core.Paging;
using Briefly.Core.Persistence;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Persistence;
using System;
using System.Linq.Expressions;

namespace Notes.Application.Notes;

public class GetNotesEndpoint(
    ILogger<GetNotesEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<Note> repository)
    : Endpoint<GetNotesRequest, PagedList<NoteDto>>
{
    public override void Configure()
    {
        Get("/notes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetNotesRequest req, CancellationToken ct)
    {
        // Define projection for Note to NoteDto
        Expression<Func<Note, NoteDto>> selector = n => new NoteDto
        {
            Id = n.Id,
            AuthorName = n.AuthorName,
            AuthorEmail = n.AuthorEmail,
            NoteTypeId = n.NoteTypeId,
            NoteTypeName = n.NoteType.Name,
            Date = n.Date,
            Mood = n.Mood,
            Energy = n.Energy,
            Summary = n.Summary,
            CreatedAt = n.CreatedAt
        };

        // Create specification with filtering and projection
        var spec = new NotesByPaginationFilterSpec<NoteDto>(req, selector);
        
        // Get paged list directly using the specification
        var items = await repository.ListAsync(spec, ct).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, ct).ConfigureAwait(false);
        
        await SendAsync(new PagedList<NoteDto>(items, req.PageNumber, req.PageSize, totalCount), cancellation: ct);
    }
}
