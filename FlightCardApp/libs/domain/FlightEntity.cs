using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    public class FlightEntity:Entity
    {
        public FlightEntity()
        {
            //DomainEvents.Add();
            //AddEvents();
            // Id zaten Entity abstract class'dan geliyor başka hiç bir yerde set edilmemesi için private set olarak Id tanımladık.
            //Id = Guid.NewGuid().ToString();
        }
    }
}
