using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.core
{
    
    /// <summary>
    /// uygulama içerisindeki tüm eventler bu interface ile işaretlensin. Domain eventler uygulama domaininde gerçekleşen bir durumu yansıtmak amaçlı bu durum ile alakalı veri taşır. Bu taşınan veriye biz event  argümans deriz. Bu arada eventler  geçmiş zaman kipinde kullanılırlar. Örneğin OrderStated, OrderCompleted, OrderShipped
    /// </summary>
    public interface IDomainEvent
    {
    }

    /// <summary>
    /// Eventleri Handler'a sevk eden birimlere ise Dispather ismi verilir.
    /// </summary>
    public interface IDomainEventDispatcher
    {
     

        //void Dispatch<TDomainEvent>(params TDomainEvent[] events) where TDomainEvent : IDomainEvent;
        void Dispatch<TDomainEvent>(TDomainEvent @event) where TDomainEvent : IDomainEvent;


    }

 

    /// <summary>
    /// Domain event handler, Eventen taşınan bilgileri alıp bir olay gerçekleştiğinde sistemin nasıl bir tepki verceğini belirlediğimiz sınıfımız.
    /// </summary>
    /// <typeparam name="TDomainEvent"></typeparam>
    public interface IDomainEventHandler<in TDomainEvent> where TDomainEvent : IDomainEvent
    {

        void Handle(TDomainEvent @event);
    }

    

    

}
