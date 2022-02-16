using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    public interface IFlighPlaningRepository:IRepository<FlightPlaning>
    {
        /// <summary>
        /// Kalkış ve varış noktası seçilmiş belirli tarihteki uçuşları getirir.
        /// </summary>
        /// <param name="flightDate"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<FlightPlaning> FilterFlightPlaning(DateTime flightDate, string from, string to);
    }
}
