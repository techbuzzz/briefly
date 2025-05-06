namespace Notes.Application.Dto;

public record NoteTypeDto
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    List<NoteFieldDefinitionDto> FieldDefinitions { get; init; } = [];
}