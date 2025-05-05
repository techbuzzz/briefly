namespace Notes.Application.NotesTypes;

public record GetNoteTypeRequest
{
    public Guid Id { get; init; }
}
