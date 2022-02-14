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
        void Dispatch<T>(params T[] events) where T : IDomainEvent;
    }

    public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
    {

        void Handle(TEvent @event);
    }

}
