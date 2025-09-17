using System.Text.Json;
using Briefly.Core.Domain;

namespace Notes.Application.Domain;

public class Note : AuditableEntity, IAggregateRoot
{
    // public string AuthorName { get; set; } = null!;
    // public string AuthorEmail { get; set; } = null!;
    public Guid NoteTypeId { get; set; }
    public NoteType NoteType { get; set; } = null!;
    public string Title { get; set; }
    public JsonDocument CustomFields { get; set; } = null!;
    // public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}