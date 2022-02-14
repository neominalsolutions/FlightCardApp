using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp.libs.core
{
    /// <summary>
    /// E-Posta göndermek için kullanacağımızı servisin contract'ı yani interface<
    /// </summary>
    public interface IEmailService
    {
        Task SendSingleEmailAsync(string to, string subject, string message);
    }
  
}
