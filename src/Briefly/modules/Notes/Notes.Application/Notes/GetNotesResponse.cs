using System.Text.Json;
using Briefly.Core.Paging;

namespace Notes.Application.Notes;

public record GetNotesResponse
{
    public required IEnumerable<NoteDto> Notes { get; init; }
    public int TotalCount { get; init; }
}
