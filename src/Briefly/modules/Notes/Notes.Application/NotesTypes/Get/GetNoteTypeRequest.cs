namespace Notes.Application.NotesTypes.Get;

public record GetNoteTypeRequest
{
    public Guid Id { get; init; }
}