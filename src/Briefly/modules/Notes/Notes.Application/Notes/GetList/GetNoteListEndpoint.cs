using Ardalis.Specification;
using Briefly.Core.Paging;
using Briefly.Core.Persistence;
using Briefly.Core.Specifications;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Notes.Specifications;
using Notes.Application.Persistence;
using System;
using System.Linq.Expressions;

namespace Notes.Application.Notes.GetList;

public class GetNoteListEndpoint(
    ILogger<GetNoteListEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<Note> repository)
    : Endpoint<GetNoteListRequest, PagedList<NoteDto>>
{
    public override void Configure()
    {
        Get("/notes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetNoteListRequest req, CancellationToken ct)
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

        // var spec = new NotesByPaginationFilterSpec<Note, NoteDto>(req);
        // Create specification with filtering and projection
        var spec = new EntitiesByPaginationFilterSpec<Note,NoteDto>(req);
        
        // Get paged list directly using the specification
        var items = await repository.ListAsync(spec, ct).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, ct).ConfigureAwait(false);
        
        await SendAsync(new PagedList<NoteDto>(items, req.PageNumber, req.PageSize, totalCount), cancellation: ct);
    }
}
