using Autofac;
using FlightCardApp.libs.core;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.infrasturcture
{

    public class NinjectDomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IContainer _kernel;

        public NinjectDomainEventDispatcher(IContainer kernel)
        {
            _kernel = kernel;
        }

        public void Dispatch<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            foreach (var handler in _kernel.Resolve<IEnumerable<IDomainEventHandler<TDomainEvent>>>())
            {
                handler.Handle(@event);
            }
        }

      
    }


    public class NetCoreEventDispatcher : IDomainEventDispatcher
    {

        /// <summary>
        /// startup dosyasında servislere erişmek için service provider interface kullandık
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// DIP ile servicelere ulaştık
        /// </summary>
        /// <param name="serviceProvider"></param>
        public NetCoreEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent
        {
            if (_serviceProvider != null)
            {

                // eğer handlerlar içerisinde net core tatrafında scoped tipinde servis tanışlanmış ise scope servisler için bu kod bloğunu yazdık. Yoksa service provider ilgili handlera erişemez. Exception fırlat.
                using (var scope = _serviceProvider.CreateScope())
                {
                    var @object = _serviceProvider.GetService<IDomainEventHandler<TDomainEvent>>();

                    foreach (var handler in _serviceProvider.GetServices<IDomainEventHandler<TDomainEvent>>())
                    {
                        // IDomainEventHandler<TDomainEvent>> bu interfaceden implemente olmuş  startudaki tüm servicleri bul.
                        // araya girip handlerların dispatcher üzerinden tetiklenmesi için kod yazdık.
                        handler.Handle(@event);
                    }
                }
            }

        }


    }
}
