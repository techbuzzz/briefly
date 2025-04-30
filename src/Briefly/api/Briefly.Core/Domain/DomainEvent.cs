namespace Briefly.Core.Domain;
public abstract record DomainEvent : IDomainEvent
{
    public DateTime RaisedOn { get; protected set; } = DateTime.UtcNow;
}
