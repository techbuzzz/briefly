using Notes.Application.Domain;

namespace Notes.Application.NotesTypes.Create;


public record CreateNoteTypeRequest(
    string Name,
    string? Description,
    List<CreateCustomFieldDefinitionRequest> FieldDefinitions
);

public record CreateCustomFieldDefinitionRequest(
    string FieldKey,
    string Label,
    FieldDataType DataType,
    bool IsRequired,
    int Order,
    List<CreateCustomFieldOptionRequest> Options
);

public record CreateCustomFieldOptionRequest(
    string Key,
    string Value
);