using Briefly.Core.Domain;

namespace Notes.Application.Domain;

public class NoteFieldDefinition: BaseEntity<Guid>
{
    public Guid NoteTypeId { get; set; }
    public string FieldKey { get; set; } = null!;
    public string Label { get; set; } = null!;
    public FieldDataType DataType { get; set; }
    public bool IsRequired { get; set; }
    public int Order { get; set; }

    // Навигация для опций (только когда DataType == Choice)
    public List<NoteFieldOption> Options { get; set; } = [];
}