using Briefly.Core.Domain;

namespace Notes.Domain.Events;

public record NoteTypeCreated : DomainEvent
{
    public required NoteType NoteType { get; set; }
}