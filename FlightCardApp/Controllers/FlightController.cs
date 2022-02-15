using FlightCardApp.libs.domain;
using FlightCardApp.libs.persistance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.Controllers
{
    public class FlightController : Controller
    {
        private AppDbContext _db;

        public FlightController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var flightPlaning = new FlightPlaning(DateTime.Now.AddDays(5));
            flightPlaning.ConnectionFlight = true;

            flightPlaning.AddFlightPrice(new FlightTypePrice(name: FlightClass.Bussiness.ToString(), listPrice: 270));
            flightPlaning.AddFlightPrice(new FlightTypePrice(name: FlightClass.Economy.ToString(), listPrice: 170));
  

            var flight1 = new Flight(
                flightCode: "1021",
                flightCompany: new Company(name: "Türk Hava Yolları", shortCode: "thy"),
                departureTime: DateTime.Now.AddDays(5).AddHours(10),
                arrivalTime: DateTime.Now.AddDays(5).AddHours(12),
                from: new Airport("İstanbul", "Sabiha Gökçen", "SAW"),
                to: new Airport("izmir", "İzmir Adnan Menderes", "ADB")
                );

            var flight2 = new Flight(
                flightCode: "1041",
                flightCompany: new Company(name: "Anadolu Jet", shortCode: "thy"),
                departureTime: DateTime.Now.AddDays(5).AddHours(14),
                arrivalTime: DateTime.Now.AddDays(5).AddHours(16),
                from: new Airport("İzmir", "Sabiha Gökçen", "ADB"),
                 to: new Airport("istanbul", "İzmir Adnan Menderes", "SAW")
                );

            flightPlaning.AddFlight(flight1);
            flightPlaning.AddFlight(flight2);

            _db.FlightPlanings.Add(flightPlaning);
            _db.SaveChanges();




            return View();
        }
    }
}
