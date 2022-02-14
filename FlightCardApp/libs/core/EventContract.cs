using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.core
{
    

    public interface IDomainEvent
    {
    }

    public interface IDomainEventDispatcher
    {
        void Dispatch<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;
    }

    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {

        void Handle(TDomainEvent @event);
    }

    public static class DomainEvent
    {
        public static IDomainEventDispatcher Dispatcher { get; set; }

        public static void Raise<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            Dispatcher.Dispatch(@event);
        }
    }

}
