using FlightCardApp.libs.core;
using FlightCardApp.libs.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.persistance
{
    public class EFFlightTicketRepository: EFBaseRepository<FlightTicket>, IFlightTicketRepository
    {
        public EFFlightTicketRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
