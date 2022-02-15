using FlightCardApp.libs.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.persistance
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<FlightPlaning> FlightPlanings { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Airport> AirPorts { get; set; }




    }
}
