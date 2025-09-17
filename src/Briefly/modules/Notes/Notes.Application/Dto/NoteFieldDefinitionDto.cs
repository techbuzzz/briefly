using Notes.Application.Domain;

namespace Notes.Application.Dto;

public record NoteFieldDefinitionDto(
    Guid Id,
    string FieldKey,
    string Label,
    FieldDataType DataType,
    bool IsRequired,
    int Order,
    List<NoteFieldOptionDto> Options
);