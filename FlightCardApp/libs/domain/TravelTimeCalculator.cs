using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    public static class TravelTimeCalculator
    {
        public static string GetTravelTime(DateTime ArrivalTime, DateTime DepartureTime)
        {

            var hourDiff = (ArrivalTime - DepartureTime).Hours;
            var minuteDiff = (ArrivalTime - DepartureTime).Minutes;

            if (hourDiff > 0 && minuteDiff > 0)
            {
                return $"{hourDiff} saat {minuteDiff} dakika"; // 3 saat 45 dakika
            }
            else if (hourDiff > 0 && minuteDiff == 0)
            {
                return $"{hourDiff} saat"; // 3 saat
            }
            else
            {
                return $"{minuteDiff} dakika"; // 45 dk
            }
        }
    }
}
