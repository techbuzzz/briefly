using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briefly.Core.Domain
{
    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; init; } = default!;

        [NotMapped]
        public Collection<DomainEvent> DomainEvents { get; } = new Collection<DomainEvent>();
        public void QueueDomainEvent(DomainEvent @event)
        {
            if (!DomainEvents.Contains(@event))
                DomainEvents.Add(@event);
        }
    }

    public abstract class BaseEntity : BaseEntity<Guid>
    {
        protected BaseEntity() => Id = Guid.NewGuid();
    }
}
