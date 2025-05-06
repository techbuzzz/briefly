namespace Notes.Application.NotesTypes.Create;

public record CreateNoteTypeRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}