using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.core
{
    /// <summary>
    /// Sistemdeki hangi sınıfın diğer sınıfları yöneteceğini bu interface kullanarak işraretleriz. Bu interfaceden türeyen sınıflar için repository açılabilir. Diğer sınıflar için gerek yok.
    /// </summary>
    public interface IAggregateRoot
    {

    }

    public interface IEntity
    {
        /// <summary>
        /// Domain Driven Design diğer bir önemli kuralı ise nesnelerimize uygulama genelinde tek bir şekilde ve anlam ifade edecek şekilde mudahale etmeliyiz.
        /// DomainEvents.Add() ve AddEvents() aynı işlemi yapan methodlar olduğundan bu iki methodun public olarak dışarı açık olması uygulamada kafa karışıklığına yol açacaktır. Bazısı DomainEvents.Add()  bazısı ise AddEvents() methodunu kullanacaktır.
        /// </summary>
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    }

    public abstract class Entity : IEntity
    {
        public string Id { get; private set; }

        private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents;

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
