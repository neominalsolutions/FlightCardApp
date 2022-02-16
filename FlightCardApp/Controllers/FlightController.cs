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
        private IFlighPlaningRepository _flighPlaningRepository;

        public FlightController(AppDbContext db, IFlighPlaningRepository flighPlaningRepository)
        {
            _db = db;
            _flighPlaningRepository = flighPlaningRepository;
        }

        public IActionResult Filter()
        {

            var data =_flighPlaningRepository.FilterFlightPlaning(DateTime.Now.AddDays(4), "ESB", "FR");

            return View();

        }

        public IActionResult Index()
        {
            var flightPlaning = new FlightPlaning(DateTime.Now.AddDays(5));
            flightPlaning.ConnectionFlight = true;

            flightPlaning.AddFlightPrice(new FlightTypePrice(name: FlightClass.Bussiness.ToString(), listPrice: 1270));
            flightPlaning.AddFlightPrice(new FlightTypePrice(name: FlightClass.Economy.ToString(), listPrice: 900));

            var izmirAirPort = new Airport("izmir", "İzmir Adnan Menderes", "ADB");
            var ankaraAirPort = new Airport("Ankara", "Esenboğa Hava Limanı", "ESB");
            var istanbulAirPort = new Airport("istanbul", "İstanbul Hava Limanı", "IST");
            var frankfurtAirPort = new Airport("Frankfurt", "Frankfurt Hava Limanı", "FR");



            var flight1 = new Flight(
                flightCode: "1021",
                flightCompany: new Company(name: "Sun Express", shortCode: "thy"),
                departureTime: DateTime.Now.AddDays(3).AddHours(10),
                arrivalTime: DateTime.Now.AddDays(3).AddHours(12),
                from: ankaraAirPort,
                to: izmirAirPort
                );

            var flight2 = new Flight(
                flightCode: "1041",
                flightCompany: new Company(name: "Anadolu Jet", shortCode: "thy"),
                departureTime: DateTime.Now.AddDays(3).AddHours(14),
                arrivalTime: DateTime.Now.AddDays(3).AddHours(16),
                from: ankaraAirPort,
                 to: istanbulAirPort
                );


            var flight3 = new Flight(
              flightCode: "1051",
              flightCompany: new Company(name: "Luftensa", shortCode: "LFT"),
              departureTime: DateTime.Now.AddDays(3).AddHours(18),
              arrivalTime: DateTime.Now.AddDays(3).AddHours(22),
              from: istanbulAirPort,
               to: frankfurtAirPort
              );

            flightPlaning.AddFlight(flight1);
            flightPlaning.AddFlight(flight2);
            flightPlaning.AddFlight(flight3);

            _db.FlightPlanings.Add(flightPlaning);
            _db.SaveChanges();






            return View();
        }
    }
}
