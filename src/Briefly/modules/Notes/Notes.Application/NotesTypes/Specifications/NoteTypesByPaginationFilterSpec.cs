using Ardalis.Specification;
using Briefly.Core.Paging;
using Briefly.Core.Specifications;
using Notes.Application.Domain;
using Notes.Application.NotesTypes.GetList;
using System;
using System.Linq.Expressions;

namespace Notes.Application.NotesTypes.Specifications;

public class NoteTypesByPaginationFilterSpec : EntitiesByPaginationFilterSpec<NoteType>
{
    public NoteTypesByPaginationFilterSpec(GetNoteTypeListRequest filter) 
        : base(filter)
    {
        // Apply custom filters
        if (!string.IsNullOrWhiteSpace(filter.NameFilter))
        {
            Query.Where(nt => nt.Name.Contains(filter.NameFilter));
        }
        
        if (filter.ActiveOnly.HasValue && filter.ActiveOnly.Value)
        {
            Query.Where(nt => nt.IsActive);
        }
        
        // Default ordering
        Query.OrderBy(nt => nt.Name);
    }
}

// public class NoteTypesByPaginationFilterSpec<TResult> : EntitiesByPaginationFilterSpec<NoteType, TResult>
// {
//     public NoteTypesByPaginationFilterSpec(GetNoteTypesRequest filter, Expression<Func<NoteType, TResult>> selector) 
//         : base(filter, selector)
//     {
//         // Apply custom filters
//         if (!string.IsNullOrWhiteSpace(filter.NameFilter))
//         {
//             Query.Where(nt => nt.Name.Contains(filter.NameFilter));
//         }
//         
//         if (filter.ActiveOnly.HasValue && filter.ActiveOnly.Value)
//         {
//             Query.Where(nt => nt.IsActive);
//         }
//         
//         // Default ordering
//         Query.OrderBy(nt => nt.Name);
//     }
// }
