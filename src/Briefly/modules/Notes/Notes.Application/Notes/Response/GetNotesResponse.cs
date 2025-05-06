using System.Text.Json;
using Briefly.Core.Paging;

namespace Notes.Application.Notes.Response;

public record GetNotesResponse
{
    public required IEnumerable<NoteDto> Notes { get; init; }
    public int TotalCount { get; init; }
}

// public sealed record GetNotesResponse(Guid? Id, string Name, string? Description);
