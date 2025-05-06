namespace Notes.Application.NotesTypes.Responses;

public record GetNoteTypesResponse
{
    public required IEnumerable<NoteTypeDto> NoteTypes { get; init; }
    public int TotalCount { get; init; }
}