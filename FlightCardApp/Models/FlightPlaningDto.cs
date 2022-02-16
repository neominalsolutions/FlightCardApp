using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.Models
{
    public class FlightPriceDto
    {
        public string Type { get; set; }
        public decimal Price { get; set; }

    }

    public class FlightDetailDto
    {
        public string From { get; set; }
        public string To { get; set; }

        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string FlightNumber { get; set; }


    }

    public class FlightPlaningDto
    {
        public string StatusText { get; set; }

        public string Id { get; set; }
        public string ConnectionType { get; set; } // aktermalı veya aktarmasız
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public string TravelTime { get; set; }



        public List<FlightPriceDto> Prices = new List<FlightPriceDto>();

        public List<FlightDetailDto> Flights { get; set; } = new List<FlightDetailDto>();

    }
}
