using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{

    /*
     * 
     * Id:string GUID // Uçuş Planlama Id; 
 List<FlightTypePrices>; // Bussiness ve Economy Fiyatı
 ConnectingFlight: boolean; // Aktarmalı mı
 Issuing Company : Company; // Uçuş planlayan şirket
 FightDate: Datetime; // Uçuş tarihi 10.10.2022
 List<Flight> Details  // Uçuştaki tüm uçuş detayı burada olucak
     * 
     */
    public class FlightPlaning:Entity
    {
        public bool ConnectionFlight { get; set; } = false;

        /// <summary>
        /// Flight Date uçuş için zorulu bir alandır. private set contructor üzerinden göndericez.
        /// </summary>
        public DateTime FlightDate { get; private set; }

        // Field da kullanıyoruz. field üzerinden set yaparız
        private List<Flight> _flights;

        /// <summary>
        /// FlightPlaning e girilen uçuşlar
        /// </summary>
        public IReadOnlyCollection<Flight> Flights // property üzerinden get yaparız
        {
            get
            {
                return _flights;
            }
        }

        private List<FlightTypePrice> _prices; // macarcase

        /// <summary>
        /// Property field bağlanmış oldu yukarıdaki get ile yazım arasında bir fark yoktur.
        /// </summary>
        public IReadOnlyCollection<FlightTypePrice> Prices => _prices;

        public FlightPlaning(DateTime flightDate)
        {
            _flights = new List<Flight>();
            _prices = new List<FlightTypePrice>();
            SetFlightDate(flightDate);  // Uçuş planlama gününü encapsulate etmiş olduk.
            
        }

        /// <summary>
        /// Minimum 2 gün sonrasına uçuş planlaması yapılabilir. 
        /// 
        /// </summary>
        private void SetFlightDate(DateTime flightDate)
        {
            var today = DateTime.Now;
            var dayDiff = (flightDate - today).Days;

            // aynı güne uçuş planlanamaz
            if(flightDate.Year == today.Year && flightDate.Month == today.Month && flightDate.Day == today.Day)
            {
                throw new Exception("Aynı gün içerisinde uçuş planlaması yapılamaz");
            }

            // minimum 2 gün sonrası için uçuş planlanmalıdır
            if(dayDiff < 2)
            {
                throw new Exception("Uçuş minimum 2 gün önceden planlanması lazım");
            }

            // planlanacak olan uçuş makismum 15 günü geçemez

            if(dayDiff > 15)
                throw new Exception("Uçuş makimum 15 gün içerisinde planlanabilir");


            FlightDate = flightDate;

        }

        /// <summary>
        /// Uçuş için gidiş dönüş tanımlaması yapmamızı sağlar. Aktarmalı ve aktarmasız uçuşlarda farklı logicler uygulanacaktır.
        /// </summary>
        /// <param name="flight"></param>
        public void AddFlight(Flight flight)
        {
            // buraya extra logicler gelecek.
            _flights.Add(flight);
        }


        /// <summary>
        /// Bir uçuşa farklı tiplerde fiyat eklememizi sağlar
        /// </summary>
        /// <param name="typePrice"></param>
        public void AddFlightPrice(FlightTypePrice typePrice)
        {
            _prices.Add(typePrice);
        }


    }
}
