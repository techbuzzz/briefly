using System.Linq.Expressions;
using Briefly.Core.Paging;
using Briefly.Core.Persistence;
using Briefly.Core.Specifications;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Dto;

namespace Notes.Application.Notes.GetList;

public class GetNoteListEndpoint(
    ILogger<GetNoteListEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)]
    IRepository<Note> repository)
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
        // Expression<Func<Note, NoteDto>> selector = n => new NoteDto
        // {
        //     Id = n.Id,
        //     NoteTypeId = n.NoteTypeId,
        //     CustomFields = n.CustomFields,
        //     CreatedAt = n.CreatedAt
        // };

        // Create specification with filtering and projection
        var spec = new EntitiesByPaginationFilterSpec<Note, NoteDto>(req);

        // Get paged list directly using the specification
        var items = await repository.ListAsync(spec, ct).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, ct).ConfigureAwait(false);

        await SendAsync(new PagedList<NoteDto>(items, req.PageNumber, req.PageSize, totalCount), cancellation: ct);
    }
}
