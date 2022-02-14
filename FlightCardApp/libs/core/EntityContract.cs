using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.core
{
    public interface IAggregateRoot
    {

    }

    public interface IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
    }

    public abstract class Entity : IEntity
    {
        public string Id { get; private set; }

        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        // sınıfa atadığımız event
        public void AddEvents(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
