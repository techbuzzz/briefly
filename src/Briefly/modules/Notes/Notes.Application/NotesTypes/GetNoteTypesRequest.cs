using Briefly.Core.Paging;

namespace Notes.Application.NotesTypes;

public class GetNoteTypesRequest : PaginationFilter
{
    public string? NameFilter { get; init; }
    public bool? ActiveOnly { get; init; }
}
