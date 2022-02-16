using FlightCardApp.libs.core;
using FlightCardApp.libs.domain;
using FlightCardApp.libs.infrasturcture;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost build = CreateHostBuilder(args).Build();
            //DomainEvent.Dispatcher = build.Services.GetRequiredService<IDomainEventDispatcher>();
            // Yani burada DomainEvent.Dispatcher propertysine sistemdeki NetrCoreEventDispacther referansýný verdik. uygulama genelinde artýk bu referans ile çalýþacaðýz.

            //DomainEvent.Raise<>(new test());

            

            build.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
