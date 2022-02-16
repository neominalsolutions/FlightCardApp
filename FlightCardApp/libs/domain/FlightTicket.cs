using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    /*
     * 
     * TicketNumber
NameOfPassenger: string (Mert Alptekin)
IdentityNumber: (TC No)
FlightNumber: string
Class (Business, Economy) : enum
Date (Uçuş Tarihi): DateTime
FlightPlaningId: string
Status (Check-in, ReadyForCheck-in, Check-out, OpenTicket): Enum
     * 
     */

    public enum FlightTicketStatus
    {
        CheckIn,
        CheckOut,
        ReadyForCheckIn,
        OpenTicket
    }

    public class FlightTicket:Entity, IAggregateRoot
    {
        public string TicketNumber { get; private set; } = Guid.NewGuid().ToString();
        public string NameOfPassenger { get; private set; }

        public string FlightClass { get; private set; }

        public string FlightPlaningId { get; set; }

        public string Email { get; private set; }

        public DateTime? FlightDate { get; set; }



        public FlightTicketStatus Status = FlightTicketStatus.ReadyForCheckIn;

        public FlightTicket()
        {

        }

        public FlightTicket(string nameOfPassenger, string flightClass, string flightPlaningId, string email,DateTime flightDate)
        {
            FlightClass = flightClass;
            NameOfPassenger = nameOfPassenger;
            FlightPlaningId = flightPlaningId;
            Email = email;
            FlightDate = flightDate;
        }


        public void ConvertOpenTicket()
        {
            Status = FlightTicketStatus.OpenTicket;
            FlightDate = null;
            TicketNumber = string.Empty;
        }

    }
}
