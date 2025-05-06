using Briefly.Core.Domain;

namespace Notes.Application.Domain;

public class NoteType : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}
//public class Note : AuditableEntity, IAggregateRoot
//{
//    public string AuthorName { get; set; } = null!;
//    public string AuthorEmail { get; set; } = null!;
//    public Guid NoteTypeId { get; set; }
//    public NoteType NoteType { get; set; } = null!;
//    public DateOnly Date { get; set; }
//    public string? Mood { get; set; }
//    public string? Energy { get; set; }
//    public string? Feeling { get; set; }
//    public string? Summary { get; set; }
//    public string? HtmlContent { get; set; }
//    public JsonDocument? RawData { get; set; }
//    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//}
//public class NoteTypeDto
//{
//    public Guid Id { get; set; }
//    public string Name { get; set; } = null!;
//    public string? Description { get; set; }
//    public bool IsActive { get; set; } = true;
//}
//public class NoteDto
//{
//    public Guid Id { get; set; }
//    public string AuthorName { get; set; } = null!;
//    public string AuthorEmail { get; set; } = null!;
//    public Guid NoteTypeId { get; set; }
//    public DateOnly Date { get; set; }
//    public string? Mood { get; set; }
//    public string? Energy { get; set; }
//    public string? Feeling { get; set; }
//    public string? Summary { get; set; }
//    public string? HtmlContent { get; set; }
//    public JsonDocument? RawData { get; set; }
//}
//public class NoteTypeCreateDto
//{
//    public string Name { get; set; } = null!;
//    public string? Description { get; set; }
//}
//public class NoteCreateDto
//{
//    public string AuthorName { get; set; } = null!;
//    public string AuthorEmail { get; set; } = null!;
//    public Guid NoteTypeId { get; set; }
//    public DateOnly Date { get; set; }
//    public string? Mood { get; set; }
//    public string? Energy { get; set; }
//    public string? Feeling { get; set; }
//    public string? Summary { get; set; }
//    public string? HtmlContent { get; set; }
//}
//public class NoteUpdateDto
//{
//    public string AuthorName { get; set; } = null!;
//    public string AuthorEmail { get; set; } = null!;
//    public Guid NoteTypeId { get; set; }
//    public DateOnly Date { get; set; }
//    public string? Mood { get; set; }
//    public string? Energy { get; set; }
//    public string? Feeling { get; set; }
//    public string? Summary { get; set; }
//    public string? HtmlContent { get; set; }
//}
//public class NoteTypeUpdateDto
//{
//    public string Name { get; set; } = null!;
//    public string? Description { get; set; }
//    public bool IsActive { get; set; } = true;
//}
//public class NoteTypeCreateRequest
//{
//    public string Name { get; set; } = null!;
//    public string? Description { get; set; }
//}
//public class NoteCreateRequest
//{
//    public string AuthorName { get; set; } = null!;
//    public string AuthorEmail { get; set; } = null!;
//    public Guid NoteTypeId { get; set; }
//    public DateOnly Date { get; set; }
//    public string? Mood { get; set; }
//    public string? Energy { get; set; }
//    public string? Feeling { get; set; }
//    public string? Summary { get; set; }
//    public string? HtmlContent { get; set; }
//}
//public class NoteUpdateRequest
//{
//    public string AuthorName { get; set; } = null!;
//    public string AuthorEmail { get; set; } = null!;
//    public Guid NoteTypeId { get; set; }
//    public DateOnly Date { get; set; }
//    public string? Mood { get; set; }
//    public string? Energy { get; set; }
//    public string? Feeling { get; set; }
//    public string? Summary { get; set; }
//    public string? HtmlContent { get; set; }
//}
//public class NoteTypeUpdateRequest
//{
//    public string Name { get; set; } = null!;
//    public string? Description { get; set; }
//    public bool IsActive { get; set; } = true;
//}
//public class NoteTypeListRequest
//{
//    public string? Name { get; set; }
//    public bool? IsActive { get; set; }
//    public int PageNumber { get; set; } = 1;
//    public int PageSize { get; set; } = 10;
//}
//public class NoteListRequest
//{
//    public string? AuthorName { get; set; }
//    public string? AuthorEmail { get; set; }
//    public Guid? NoteTypeId { get; set; }
//    public DateOnly? Date { get; set; }
//    public string? Mood { get; set; }
//    public string? Energy { get; set; }
//    public string? Feeling { get; set; }
//    public string? Summary { get; set; }
//    public string? HtmlContent { get; set; }
//    public int PageNumber { get; set; } = 1;
//    public int PageSize { get; set; } = 10;
//}
//public class NoteTypeListResponse
//{
//    public List<NoteTypeDto> NoteTypes { get; set; } = new List<NoteTypeDto>();
//    public int TotalCount { get; set; }
//}
//public class NoteListResponse
//{
//    public List<NoteDto> Notes { get; set; } = new List<NoteDto>();
//    public int TotalCount { get; set; }
//}
//public class NoteTypeResponse
//{
//    public NoteTypeDto NoteType { get; set; } = null!;
//}
//public class NoteResponse
//{
//    public NoteDto Note { get; set; } = null!;
//}
//public class NoteTypeCreateResponse
//{
//    public NoteTypeDto NoteType { get; set; } = null!;
//}
//public class NoteCreateResponse
//{
//    public NoteDto Note { get; set; } = null!;
//}
//public class NoteTypeUpdateResponse
//{
//    public NoteTypeDto NoteType { get; set; } = null!;
//}
//public class NoteUpdateResponse
//{
//    public NoteDto Note { get; set; } = null!;
//}
//public class NoteTypeDeleteResponse
//{
//    public bool IsDeleted { get; set; }
//}
//public class NoteDeleteResponse
//{
//    public bool IsDeleted { get; set; }
//}
//public class NoteTypeDeleteRequest
//{
//    public Guid Id { get; set; }
//}