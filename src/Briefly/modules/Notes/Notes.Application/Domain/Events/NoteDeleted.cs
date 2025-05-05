using Briefly.Core.Domain;

namespace Notes.Application.Domain.Events;

public record NoteDeleted : DomainEvent
{
    public required Guid NoteId { get; init; }
    public required string AuthorName { get; init; }
}
