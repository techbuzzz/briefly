namespace Notes.Application.NotesTypes;

public record NoteTypeDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
}