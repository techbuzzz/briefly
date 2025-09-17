using System.Text.Json;

namespace Notes.Application.Notes.Update;

public record UpdateNoteRequest(
    Guid Id,
    JsonElement CustomFields
);