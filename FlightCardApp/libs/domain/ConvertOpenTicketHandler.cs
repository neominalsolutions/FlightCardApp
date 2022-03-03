using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    /// <summary>
    /// Satın alınan biletleri OpenTicket State'e çekmek için Bileti açığa alacağız. FlightCanceled event Subscribe olan ConvertOpenTicketHandler subscriber ismini veriyoruz.
    /// </summary>
    public class ConvertOpenTicketHandler : IDomainEventHandler<FlightCanceled>
    {
        /// <summary>
        /// Flight Canceled olduğunda yapılacak olan işlemleri burada işleyeceğiz.
        /// </summary>
        /// <param name="event"></param>
        public void Handle(FlightCanceled @event)
        {

            
        }

      
    }
}
