using Briefly.Core.Paging;

namespace Notes.Application.Notes.GetList;

public class GetNoteListRequest : PaginationFilter
{
    public Guid? NoteTypeId { get; init; }
}