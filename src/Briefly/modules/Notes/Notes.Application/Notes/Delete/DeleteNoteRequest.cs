namespace Notes.Application.Notes.Delete;

public record DeleteNoteRequest
{
    public Guid Id { get; init; }
}
