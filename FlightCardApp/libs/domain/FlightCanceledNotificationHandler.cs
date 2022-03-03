using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    /// <summary>
    /// Bu uçuş ile ilgili bilet satın alan kişilere mail atar.
    /// </summary>
    public class FlightCanceledNotificationHandler: IDomainEventHandler<FlightCanceled>
    {

        public void Handle(FlightCanceled @event)
        {
            // tüm mail gönderilecek olan kişileri bulup, aşağıdaki methodu çalıştıracağız.
            //_emailService.SendSingleEmailAsync("test","deneme","deneme");
        }
    }
}
