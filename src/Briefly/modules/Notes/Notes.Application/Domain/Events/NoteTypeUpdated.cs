using Briefly.Core.Domain;

namespace Notes.Application.Domain.Events;

public record NoteTypeUpdated : DomainEvent
{
    public required NoteType NoteType { get; init; }
}