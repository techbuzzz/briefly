using Briefly.Core.Paging;

namespace Notes.Application.Notes.GetList;

public class GetNoteListRequest : PaginationFilter
{
    public string? AuthorNameFilter { get; init; }
    public string? AuthorEmailFilter { get; init; }
    public Guid? NoteTypeId { get; init; }
    public DateOnly? FromDate { get; init; }
    public DateOnly? ToDate { get; init; }
    public string? ContentSearch { get; init; }
}
