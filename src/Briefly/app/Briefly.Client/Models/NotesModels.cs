using System.Text.Json;

namespace Briefly.Client.Models;

public class NoteDto
{
    public Guid Id { get; set; }
    public Guid NoteTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public JsonDocument CustomFields { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class NoteTypeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public List<NoteFieldDefinitionDto> FieldDefinitions { get; set; } = [];
}

public class NoteFieldDefinitionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public FieldDataType DataType { get; set; }
    public bool IsRequired { get; set; }
    public string? DefaultValue { get; set; }
    public int DisplayOrder { get; set; }
    public List<NoteFieldOptionDto> Options { get; set; } = [];
}

public class NoteFieldOptionDto
{
    public Guid Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
}

public enum FieldDataType
{
    Text,
    Number,
    Date,
    Boolean,
    Select,
    MultiSelect,
    TextArea,
    Email,
    Url
}

public class CreateNoteRequest
{
    public Guid NoteTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Dictionary<string, object> CustomFields { get; set; } = [];
}

public class UpdateNoteRequest
{
    public Guid Id { get; set; }
    public Guid NoteTypeId { get; set; }
    public string Title { get; set; } = string.Empty;
    public Dictionary<string, object> CustomFields { get; set; } = [];
}

public class CreateNoteTypeRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdateNoteTypeRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
}

public class PaginationRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
}

public class GetNotesResponse
{
    public List<NoteDto> Notes { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

public class GetNoteTypesResponse
{
    public List<NoteTypeDto> NoteTypes { get; set; } = [];
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}