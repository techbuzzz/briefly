namespace Notes.Application.NotesTypes.Update;

public record UpdateNoteTypeRequest
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; } = true;
}