using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    /// <summary>
    /// Event Immutable Nesnedir sadece tek bir şekilde değerlerin değişimi olabilir. O sebep ile private set yapıp dışarıdan property geçilmesine izin vermiyoruz. Uçuş iptal olduğunda taşınacak olan değerleri içerisinde argüman olarak barındırır.
    /// </summary>
    public class FlightCanceled: IDomainEvent
    {
        public string FlightPlaningId { get; set; }
        public string CancelationReason { get; set; }


        public FlightCanceled(string flightPlaningId, string cancelationReason)
        {
            FlightPlaningId = flightPlaningId;
            CancelationReason = cancelationReason;
        }

    }
}
