using System.Text.Json;

namespace Notes.Application.Dto;

public record NoteDto(
    Guid Id,
    Guid NoteTypeId,
    JsonDocument CustomFields,
    DateTime CreatedAt
);