using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    /*
     * 
     * Flight Number : string; (Key) Uçuş Numarası
From : AirPort           Kalkacağı Havalimanı
To: AirPort             Ineceği Havalimanı
Company: Company        Hangi Şirkete ait bir uçuş olduğu
DepartureTime: DateTime Kalkış zamanı
ArrivalTime : DateTime  Varış zamanı
     * 
     */

    /// <summary>
    /// Value Object: Kendisine ait Id si olmayan ama domain içerisinde bir entity'nin ihtiyacını karşılamak için kendi başına bir nesne özelliği gösteren sınıflara ise ValueObject ismini veriyoruz. Flight için FlightRoute bilgisi tek başına bir anlam ifade etmektedir. Bu sebep ile bu nesneyi valueObject olarak kullandık.
    /// </summary>
    public class FlightRoute
    {
        public Airport From { get; private set; } // Nereden SAW
        public Airport To { get; private set; } // Nereye ADB

        public FlightRoute(Airport from, Airport to)
        {
            From = from;
            To = to;
        }
    }

    public class Flight:Entity
    {
        public Flight()
        {

        }

        public string FlightNumber { get; private set; }
        public Company FlightCompany { get; private set; }

        //FlightRoute Route { get; set; }

        public Airport From { get; private set; } // Nereden SAW
        public Airport To { get; private set; } // Nereye ADB


     

        /// <summary>
        /// Varış zamanı
        /// </summary>
        public DateTime ArrivalTime { get; private set; } // 29 Mart 2022 Salı 16:30

        /// <summary>
        /// Kalkış zamanı
        /// </summary>
        public DateTime DepartureTime { get; private set; } // 28 Mart 2022 Pazartesi 21:45

        /// <summary>
        /// Yolculuk Süresi
        /// </summary>
        public string TravelTime
        {
            get
            {
                return TravelTimeCalculator.GetTravelTime(ArrivalTime, DepartureTime);
            }
        }



        // Flight Number Generate edeceğiz
        // flight Code
        // CompanyShortCode THY-FlightCode

        /// <summary>
        /// FlightCode 1021 numaralı uçuş bilgisini operasyon çalışanı belirtmelidir.
        /// </summary>
        /// <param name="flightCode"></param>
        public Flight(string flightCode, Company flightCompany, DateTime departureTime, DateTime arrivalTime, Airport from, Airport to)
        {
            FlightCompany = flightCompany;
            GenerateFlightNumber(flightCode);
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            From = from;
            To = to;
        }



        /// <summary>
        /// Flight Number bilgisini oluşturduk.
        /// flightCode bilgisine sadece FlightNumber oluşturmak için ihtiyacımız olucak veritabanında saklamamız gerekmez.
        /// </summary>
        /// <param name="flightCode"></param>
        private void GenerateFlightNumber(string flightCode)
        {
            if (string.IsNullOrEmpty(flightCode))
            {
                throw new Exception("Uçuş kodu boş girildi!");
            }

            // THY-1167 AND
            FlightNumber = $"{FlightCompany.ShortCode}-{flightCode}";
        }

    }
}
