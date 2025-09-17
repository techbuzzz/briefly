namespace Notes.Application.Notes.Get;

public record GetNoteRequest
{
    public Guid Id { get; init; }
}