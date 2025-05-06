using Notes.Application.NotesTypes.Create;

namespace Notes.Application.NotesTypes.Update;

public record UpdateNoteTypeRequest(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive,
    List<CreateCustomFieldDefinitionRequest> FieldDefinitions
);