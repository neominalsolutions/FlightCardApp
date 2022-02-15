using FlightCardApp.libs.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.domain
{
    public class Company:Entity
    {

        public Company()
        {

        }
 

        public string Name { get; private set; } // Türk Hava yolları
        public string ShortCode { get; private set; } // THY

        public Company(string name, string shortCode)
        {
            SetName(name);
            SetShortCode(shortCode);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new Exception("Company Name boş geçildi");

            Name = name.Trim();
        }

        public void SetShortCode(string shortCode)
        {
            if (string.IsNullOrEmpty(shortCode))
                throw new Exception("Company ShortCode boş geçildi");

            ShortCode = shortCode.Trim().ToUpper();
        }


    }
}
