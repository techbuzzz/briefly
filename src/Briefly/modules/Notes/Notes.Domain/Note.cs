using System.Text.Json;
using Briefly.Core.Domain;

namespace Notes.Domain
{
    public class Note : AuditableEntity, IAggregateRoot
    {
        public string AuthorName { get; set; } = null!;
        public string AuthorEmail { get; set; } = null!;
        public Guid NoteTypeId { get; set; }
        public NoteType NoteType { get; set; } = null!;
        public DateOnly Date { get; set; }
        public string? Mood { get; set; }
        public string? Energy { get; set; }
        public string? Feeling { get; set; }
        public string? Summary { get; set; }
        public string? HtmlContent { get; set; }
        public JsonDocument? RawData { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
