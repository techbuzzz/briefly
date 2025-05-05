using Briefly.Core.Paging;

namespace Notes.Application.NotesTypes;

public record GetNoteTypesResponse
{
    public required IEnumerable<NoteTypeDto> NoteTypes { get; init; }
    public int TotalCount { get; init; }
}