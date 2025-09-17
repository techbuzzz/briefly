using Briefly.Core.Domain;

namespace Notes.Application.Domain;

public class NoteType : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Note> Notes { get; set; } = new List<Note>();

    public ICollection<NoteFieldDefinition> FieldDefinitions { get; set; } = [];
}