namespace Notes.Application.NotesTypes.Delete;

public record DeleteNoteTypeRequest
{
    public Guid Id { get; init; }
}
