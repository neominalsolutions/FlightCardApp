using FlightCardApp.libs.domain;
using FlightCardApp.libs.persistance;
using FlightCardApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.Controllers
{
    public class FlightController : Controller
    {
        //private AppDbContext _db;
        private IFlighPlaningRepository _flighPlaningRepository;
        private IFlightTicketRepository _flightTicketRepository;

        public FlightController(IFlighPlaningRepository flighPlaningRepository, IFlightTicketRepository flightTicketRepository)
        {
            //_db = db;
            _flighPlaningRepository = flighPlaningRepository;
            _flightTicketRepository = flightTicketRepository;
        }

        /// <summary>
        /// Biz burada bilet almak için kayıt gönderelim
        /// </summary>
        /// <param name="className"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public IActionResult SelectFlight(string className, string code)
        {

            var flightPlaning = _flighPlaningRepository.Find(code);

            var flightTicket = new FlightTicket("mert alptekin", className, code, "mert.alptekin@neominal.com", flightPlaning.FlightDate);

            var flightTicket2 = new FlightTicket("ali tan", className, code, "test@test.com", flightPlaning.FlightDate);

            _flightTicketRepository.Add(flightTicket);
            _flightTicketRepository.Add(flightTicket2);

            _flightTicketRepository.Save();


            return View();
        }


        public IActionResult Cancel(string id)
        {
            var flightPlaning = _flighPlaningRepository.Find(id);
            flightPlaning.FlightCanceled("Kötü hava koşulları");

            _flighPlaningRepository.Save();

            return RedirectToAction("List");
        }

        public IActionResult Submit(string id)
        {
            var flightPlaning =  _flighPlaningRepository.Find(id);
            flightPlaning.FlightSubmit();
            _flighPlaningRepository.Save();

            return RedirectToAction("List");
        }

        public IActionResult Close(string id)
        {
            var flightPlaning = _flighPlaningRepository.Find(id);
            flightPlaning.FlightClosed();
            _flighPlaningRepository.Save();

            return RedirectToAction("List");
        }



        public IActionResult List()
        {
            var data = _flighPlaningRepository.FilterFlightPlaning(DateTime.Now.AddDays(4), "ÇKALE", "VAN");

            var model = _flighPlaningRepository.FilterFlightPlaning(DateTime.Now.AddDays(4), "ÇKALE", "VAN").Select(a => new FlightPlaningDto
            {
                StatusText = a.Status.ToString(),
                Id = a.Id,
                DepartureTime = a.Flights.OrderBy(x => x.DepartureTime).Take(1).FirstOrDefault().DepartureTime.ToShortTimeString(),
                ArrivalTime = a.Flights.OrderByDescending(x => x.ArrivalTime).Take(1).FirstOrDefault().ArrivalTime.ToShortTimeString(),
                ConnectionType = a.ConnectionFlight ?  $"{a.Flights.Count() - 1 } aktarmalı uçuş" : "direkt uçuş",
                From = a.FromCode,
                To = a.ToCode,
                //TravelTime = TravelTimeCalculator(a.Flights.OrderBy(x=> x.DepartureTime).Take(1).FirstOrDefault().DepartureTime, a.Flights.OrderByDescending(x=> x.ArrivalTime).Take(1).FirstOrDefault().ArrivalTime),
                TravelTime = a.TotalTravelTime,
                Flights = a.Flights.Select(c => new FlightDetailDto
                {
                    ArrivalTime = c.ArrivalTime.ToShortTimeString(),
                    DepartureTime = c.DepartureTime.ToShortTimeString(),
                    FlightNumber = c.FlightNumber,
                    From = $"{c.From?.ShortCode}-{c.From?.Name}",
                    To = $"{c.To?.ShortCode}-{c.To?.Name}"

                }).ToList(),
                Prices = a.Prices.Select(d => new FlightPriceDto
                {
                    Price = d.ListPrice,
                    Type = d.Name
                }).ToList()

            }).ToList();


            return View(model);
        }


        private string TravelTimeCalculator(DateTime departureTime, DateTime arrivalTime)
        {

            int hours =  (arrivalTime - departureTime).Hours;
            int seconds = (arrivalTime - departureTime).Seconds;

            if (hours == 0)
            {
                return $"{seconds} dakika";
            }
            else 
            {
                return $"{hours} saat {seconds} dakika" ;
            }
        }

        public IActionResult Filter()
        {

            var model =_flighPlaningRepository.FilterFlightPlaning(DateTime.Now.AddDays(4), "ÇKALE", "VAN").Select(a=> new FlightPlaningDto {

                DepartureTime = a.Flights.OrderBy(x=> x.DepartureTime).Take(1).FirstOrDefault().DepartureTime.ToShortTimeString(),
                ArrivalTime = a.Flights.OrderByDescending(x => x.ArrivalTime).Take(1).FirstOrDefault().ArrivalTime.ToShortTimeString(),
                ConnectionType = a.ConnectionFlight ? "direkt uçuş": $"{a.Flights.Count() -1 } aktarmalı uçuş",
                From = a.FromCode,
                To = a.ToCode,
                //TravelTime = TravelTimeCalculator(a.Flights.OrderBy(x=> x.DepartureTime).Take(1).FirstOrDefault().DepartureTime, a.Flights.OrderByDescending(x=> x.ArrivalTime).Take(1).FirstOrDefault().ArrivalTime),
                TravelTime = a.TotalTravelTime,
                Flights = a.Flights.Select(c=> new FlightDetailDto
                {
                    ArrivalTime  = c.ArrivalTime.ToShortTimeString(),
                    DepartureTime = c.DepartureTime.ToShortTimeString(),
                    FlightNumber = c.FlightNumber,
                    From = $"{c.From.ShortCode}-{c.From.Name}",
                    To = $"{c.To.ShortCode}-{c.To.Name}"
                   
                }).ToList(),
                Prices = a.Prices.Select(d =>  new FlightPriceDto
                {
                    Price = d.ListPrice,
                    Type = d.Name
                }).ToList()

            }).ToList();



            return View();

        }

        public IActionResult Index()
        {
            var flightPlaning = new FlightPlaning(DateTime.Now.AddDays(3));
            flightPlaning.ConnectionFlight = false;

            flightPlaning.AddFlightPrice(new FlightTypePrice(name: FlightClass.Bussiness.ToString(), listPrice: 600));
            flightPlaning.AddFlightPrice(new FlightTypePrice(name: FlightClass.Economy.ToString(), listPrice: 250));

            //var antalyaAirPort = new Airport("Hatay", "Antakya Hava Limanı", "ANTK");
            var canakaleAirPort = new Airport("Çanakkale", "Çanakkale Hava Limanı", "ÇKALE");
            var vanAirPort = new Airport("Van", "Van Hava Limanı", "Van");
            //var bursaAirPort = new Airport("Bursa", "Bursa Yenişehir Hava Limanı", "BUR");
            //var ankaraAirPort = new Airport("Ankara", "Esenboğa Hava Limanı", "ESB");
            //var istanbulAirPort = new Airport("istanbul", "İstanbul Hava Limanı", "IST");
            //var frankfurtAirPort = new Airport("Frankfurt", "Frankfurt Hava Limanı", "FR");



            var flight1 = new Flight(
                flightCode: "1091",
                flightCompany: new Company(name: "Bora Jet", shortCode: "thy"),
                departureTime: DateTime.Now.AddDays(3).AddHours(10),
                arrivalTime: DateTime.Now.AddDays(3).AddHours(12),
                from: canakaleAirPort,
                to: vanAirPort
                );


            flightPlaning.AddFlight(flight1);



            _flighPlaningRepository.Add(flightPlaning);
            _flighPlaningRepository.Save();


            return View();
        }
    }
}
