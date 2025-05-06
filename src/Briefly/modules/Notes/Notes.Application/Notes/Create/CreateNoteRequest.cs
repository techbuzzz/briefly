using System.Text.Json;

namespace Notes.Application.Notes.Create;

public record CreateNoteRequest
{
    public required Guid NoteTypeId { get; init; }
    public required JsonElement CustomFields { get; init; }
}