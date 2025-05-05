namespace Notes.Application.Notes;

public record DeleteNoteRequest
{
    public Guid Id { get; init; }
}
