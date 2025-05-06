namespace Notes.Application.NotesTypes.Responses;

public record GetNoteTypeResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
}