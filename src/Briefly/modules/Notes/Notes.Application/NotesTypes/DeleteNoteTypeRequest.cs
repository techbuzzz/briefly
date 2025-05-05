namespace Notes.Application.NotesTypes;

public record DeleteNoteTypeRequest
{
    public Guid Id { get; init; }
}
