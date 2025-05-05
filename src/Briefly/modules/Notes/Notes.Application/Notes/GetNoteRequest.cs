namespace Notes.Application.Notes;

public record GetNoteRequest
{
    public Guid Id { get; init; }
}
