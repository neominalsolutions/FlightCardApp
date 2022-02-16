using FlightCardApp.libs.core;
using FlightCardApp.libs.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.persistance
{
    public class EFFlightPlaningRepository: EFBaseRepository<FlightPlaning>, IFlighPlaningRepository
    {
        public EFFlightPlaningRepository(AppDbContext appDbContext):base(appDbContext)
        {

        }

        public List<FlightPlaning> FilterFlightPlaning(DateTime flightDate, string from, string to)
        {
            //var flightPlanings = _dbSet.Include(x => x.Flights).ThenInclude(x => x.To).Include(x => x.Flights).ThenInclude(x => x.From).Where(x => x.FlightDate <= flightDate).ToList();



            //return flightPlanings.Where(x=> x.FromCode == from && x.ToCode == to).ToList();

            return _dbSet.Include(x => x.Prices).Include(x => x.Flights).ThenInclude(x => x.To).Include(x => x.Flights).ThenInclude(x => x.From).Where(x => x.FlightDate <= flightDate && x.ToCode == to && x.FromCode == from).ToList();
        }
    }
}
