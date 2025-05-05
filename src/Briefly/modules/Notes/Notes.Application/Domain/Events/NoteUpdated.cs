using Briefly.Core.Domain;

namespace Notes.Application.Domain.Events;

public record NoteUpdated : DomainEvent
{
    public required Note Note { get; init; }
}
