using FlightCardApp.libs.domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.Controllers
{
    /*
    public class Bank
    {

    }

    public class ABank:IBank
    {
        public void Pay()
        {

        }
    }

    public class BBank:IBank
    {
        public void Pay()
        {

        }
    }

    public interface IBank
    {
        void Pay();
    }

    public class Bank3Service
    {
        private IBank _bank;
        
        public Bank3Service(IBank bank)
        {
            _bank = bank;
        }

        public void Pay()
        {
            _bank.Pay();

        }

    }

    public class Bank2Service
    {
        private ABank _aBank;
        private BBank _bBank;
        public Bank2Service(ABank aBank)
        {
            _aBank = aBank;
        }

        public Bank2Service(BBank bBank)
        {
            _bBank = bBank;
        }

        public void Pay()
        {
            if (_aBank != null)
            {
                _aBank.Pay();
            }
            else
            {
                _bBank.Pay();
            }


        }
    }



    public class BankService
    {
        private ABank aBank = new ABank();
        private BBank bBank = new BBank();

        public void Pay(Type bankType)
        {
            if(typeof(ABank) == bankType)
            {
                aBank.Pay();
            }
            else
            {
                bBank.Pay();
            }

            
        }
    }

    */

    public class HomeController : Controller
    {
        //private IConfiguration configuration1;

        public HomeController(/*IConfiguration configuration*/)
        {
            //configuration1 = configuration;
        }
        public IActionResult Index()
        {

            var company = new Company(name: " Türk havayolları ", shortCode: " thy ");



            company.SetShortCode("AND");
            company.SetName("Türk Hava Yolları");

            var flightPlaning = new FlightPlaning(DateTime.Now);
            //flightPlaning.



            /*
            // tight coupling sıkı sıkıya bağlılık.
            Bank2Service b2 = new Bank2Service(new ABank());
            b2.Pay();

            Bank2Service b3 = new Bank2Service(new BBank());
            b3.Pay();

            BankService bs = new BankService();
            bs.Pay(typeof(ABank));

            // loose coupling zayıf bağlılık
            Bank3Service bs3 = new Bank3Service(new ABank());
            bs3.Pay(); // A Bankası
            Bank3Service bs32 = new Bank3Service(new BBank());
            bs32.Pay(); // B Bankası
            // DI + DIP (Dependency Inversion)

            */
            return View();
        }
    }
}
