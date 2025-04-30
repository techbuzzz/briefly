using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.NotesTypes
{
    public record CreateNoteTypeRequest
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
