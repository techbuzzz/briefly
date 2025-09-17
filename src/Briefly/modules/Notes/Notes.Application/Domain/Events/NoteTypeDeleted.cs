using Briefly.Core.Domain;

namespace Notes.Application.Domain.Events;

public record NoteTypeDeleted : DomainEvent
{
    public required Guid NoteTypeId { get; init; }
    public required string NoteTypeName { get; init; }
}