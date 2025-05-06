using System.Text.Json;

namespace Notes.Application.Notes.Response;

public record GetNoteResponse
{
    public required Guid Id { get; init; }
    public required string AuthorName { get; init; }
    public required string AuthorEmail { get; init; }
    public required Guid NoteTypeId { get; init; }
    public required string NoteTypeName { get; init; }
    public required DateOnly Date { get; init; }
    public string? Mood { get; init; }
    public string? Energy { get; init; }
    public string? Feeling { get; init; }
    public string? Summary { get; init; }
    public string? HtmlContent { get; init; }
    public JsonDocument? RawData { get; init; }
    public DateTime CreatedAt { get; init; }
}