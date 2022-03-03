using Autofac;
using FlightCardApp.libs.core;
using FlightCardApp.libs.domain;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FlightCardApp.libs.infrasturcture
{

    public class AutofacDomainEventDispatcher : IDomainEventDispatcher
    {
        // Autofac üzerinde register olan arkadaşlarımızı IComponentContext interface ile Autofacten gelir ServiceProvider gibi çalışır Resolve et
        private readonly IComponentContext _context;

        public AutofacDomainEventDispatcher(IComponentContext context)
        {
            _context = context;
        }

        public void Dispatch<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
           
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());
            // bir event birden fazla Handler çalıştırabilir.
            var handlersCollectionType = typeof(IEnumerable<>).MakeGenericType(handlerType);

            try
            {
                //c# 4.0 ile dynamic geldi.
                dynamic handlers = _context.Resolve(handlersCollectionType);
                

                if (handlers == null)
                    return;

                foreach (dynamic handler in handlers)
                {
                    dynamic dynamicEvent = @event;
                    handler.Handle(dynamicEvent);

                }
            }
            catch (RuntimeBinderException)
            {
                    
                throw new RuntimeBinderException("Dynamic tipine çeviriken bir hata oluştu");
            }

           
        }

        
    }

    
}
