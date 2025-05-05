using Briefly.Core.Domain;

namespace Notes.Application.Domain.Events;

public record NoteTypeCreated : DomainEvent
{
    public required NoteType NoteType { get; set; }
}