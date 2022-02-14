using FlightCardApp.libs.core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.infrasturcture
{

    public class NetCoreEventDispatcher : IDomainEventDispatcher
    {

        private readonly IServiceProvider _serviceProvider;

        public NetCoreEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            if (_serviceProvider != null)
            {

                using (var scope = _serviceProvider.CreateScope())
                {

                    foreach (var handler in scope.ServiceProvider.GetServices<IDomainEventHandler<TDomainEvent>>())
                    {

                        handler.Handle(@event);
                    }
                }
            }

        }

      
    }
}
