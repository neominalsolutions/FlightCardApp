using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    public class FlightTypePrice:Entity
    {

        public FlightTypePrice()
        {

        }

        public string Name { get; private set; } // Economy
        public decimal ListPrice { get; set; } // 100 TL


        public FlightTypePrice(string name, decimal listPrice)
        {
            SetName(name);
            SetListPrice(listPrice);
        }

        private void SetListPrice(decimal price)
        {
            if(price <= 0)
            {
                throw new Exception("Fiyat 0 veya negatif değer olamaz");
            }

            ListPrice = price;
        }

        private void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Fiyat Tarifesi boş geçilemez");
            }


            var names =  Enum.GetNames(typeof(FlightClass)).ToList();


            if (!names.Contains(name))
            {
                throw new Exception("Böyle bir fiyat tarifesi bulunamadı!");
            }

            Name = name;
        }



    }
}
