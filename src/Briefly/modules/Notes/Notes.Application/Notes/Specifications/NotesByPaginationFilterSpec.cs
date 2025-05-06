using Ardalis.Specification;
using Briefly.Core.Specifications;
using Notes.Application.Domain;
using Notes.Application.Notes.GetList;

namespace Notes.Application.Notes.Specifications;

public class NotesByPaginationFilterSpec : EntitiesByPaginationFilterSpec<Note>
{
    public NotesByPaginationFilterSpec(GetNoteListRequest filter)
        : base(filter)
    {
        Query.Include(n => n.NoteType);

        // Apply custom filters
        if (!string.IsNullOrWhiteSpace(filter.AuthorNameFilter))
            Query.Where(n => n.AuthorName.Contains(filter.AuthorNameFilter));

        if (!string.IsNullOrWhiteSpace(filter.AuthorEmailFilter))
            Query.Where(n => n.AuthorEmail.Contains(filter.AuthorEmailFilter));

        if (filter.NoteTypeId.HasValue) Query.Where(n => n.NoteTypeId == filter.NoteTypeId.Value);

        if (filter.FromDate.HasValue) Query.Where(n => n.Date >= filter.FromDate.Value);

        if (filter.ToDate.HasValue) Query.Where(n => n.Date <= filter.ToDate.Value);

        if (!string.IsNullOrWhiteSpace(filter.ContentSearch))
            Query.Where(n =>
                (n.Summary != null && n.Summary.Contains(filter.ContentSearch)) ||
                (n.HtmlContent != null && n.HtmlContent.Contains(filter.ContentSearch)));

        // Default ordering
        Query.OrderByDescending(n => n.Date)
            .ThenByDescending(n => n.CreatedAt);
    }
}

// public class NotesByPaginationFilterSpec<TResult> : EntitiesByPaginationFilterSpec<Note, TResult>
// {
//     public NotesByPaginationFilterSpec(GetNotesRequest filter, Expression<Func<Note, TResult>> selector) 
//         : base(filter, selector)
//     {
//         Query.Include(n => n.NoteType);
//         
//         // Apply custom filters
//         if (!string.IsNullOrWhiteSpace(filter.AuthorNameFilter))
//         {
//             Query.Where(n => n.AuthorName.Contains(filter.AuthorNameFilter));
//         }
//         
//         if (!string.IsNullOrWhiteSpace(filter.AuthorEmailFilter))
//         {
//             Query.Where(n => n.AuthorEmail.Contains(filter.AuthorEmailFilter));
//         }
//         
//         if (filter.NoteTypeId.HasValue)
//         {
//             Query.Where(n => n.NoteTypeId == filter.NoteTypeId.Value);
//         }
//         
//         if (filter.FromDate.HasValue)
//         {
//             Query.Where(n => n.Date >= filter.FromDate.Value);
//         }
//         
//         if (filter.ToDate.HasValue)
//         {
//             Query.Where(n => n.Date <= filter.ToDate.Value);
//         }
//         
//         if (!string.IsNullOrWhiteSpace(filter.ContentSearch))
//         {
//             Query.Where(n => 
//                 (n.Summary != null && n.Summary.Contains(filter.ContentSearch)) || 
//                 (n.HtmlContent != null && n.HtmlContent.Contains(filter.ContentSearch)));
//         }
//         
//         // Default ordering
//         Query.OrderByDescending(n => n.Date)
//             .ThenByDescending(n => n.CreatedAt);
//     }
// }