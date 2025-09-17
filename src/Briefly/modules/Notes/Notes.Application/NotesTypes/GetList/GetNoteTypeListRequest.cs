using Briefly.Core.Paging;

namespace Notes.Application.NotesTypes.GetList;

public class GetNoteTypeListRequest : PaginationFilter
{
    public string? NameFilter { get; init; }
    public bool? ActiveOnly { get; init; }
}