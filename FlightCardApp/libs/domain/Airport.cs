using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    public class Airport:Entity
    {

        public Airport()
        {

        }

        public string Name { get; private set; } // Sabiha Gökçen
        public string ShortCode { get; private set; } // SAW

        public string CityName { get; private set; } // istanbul


        public Airport(string cityName, string name, string shortCode)
        {
            ShortCode = shortCode;
            CityName = cityName;
            Name = name;
        }


    }
}
