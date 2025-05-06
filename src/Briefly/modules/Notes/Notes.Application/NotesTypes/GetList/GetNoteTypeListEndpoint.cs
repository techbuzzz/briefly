using Ardalis.Specification;
using Briefly.Core.Paging;
using Briefly.Core.Persistence;
using Briefly.Core.Specifications;
using FastEndpoints;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Notes.Application.Domain;
using Notes.Application.Persistence;
using System;
using System.Linq.Expressions;

namespace Notes.Application.NotesTypes.GetList;

public class GetNoteTypeListEndpoint(
    ILogger<GetNoteTypeListEndpoint> logger,
    [FromKeyedServices(NotesMetadata.DIKey)] IRepository<NoteType> repository) 
    : Endpoint<GetNoteTypeListRequest, IPagedList<NoteTypeDto>>
{
    public override void Configure()
    {
        Get("/note-types");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetNoteTypeListRequest req, CancellationToken ct)
    {
        // Define projection for NoteType to NoteTypeDto
        Expression<Func<NoteType, NoteTypeDto>> selector = nt => new NoteTypeDto
        {
            Id = nt.Id,
            Name = nt.Name,
            Description = nt.Description,
            IsActive = nt.IsActive
        };

        // Create specification with filtering and projection
        var spec = new EntitiesByPaginationFilterSpec<NoteType, NoteTypeDto>(req);
        
        // Get paged list directly using the specification
        var items = await repository.ListAsync(spec, ct).ConfigureAwait(false);
        var totalCount = await repository.CountAsync(spec, ct).ConfigureAwait(false);

        await SendAsync(new PagedList<NoteTypeDto>(items, req.PageNumber, req.PageSize, totalCount), cancellation: ct);

    }
}
