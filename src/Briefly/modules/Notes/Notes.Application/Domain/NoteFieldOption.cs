using Briefly.Core.Domain;

namespace Notes.Application.Domain;

public class NoteFieldOption : BaseEntity<Guid>
{
    public Guid FieldDefinitionId { get; set; }
    public string Key { get; set; } = null!;    // e.g. "low", "medium", "high"
    public string Value { get; set; } = null!;  // e.g. "Low", "Medium", "High"
}