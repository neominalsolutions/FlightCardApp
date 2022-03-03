using Autofac;
using Autofac.Extensions.DependencyInjection;
using FlightCardApp.libs.core;
using FlightCardApp.libs.domain;
using FlightCardApp.libs.infrasturcture;
using Microsoft.AspNetCore;
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
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new RegisterModule());
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                  webBuilder.UseStartup<Startup>();
            });
          
    }
}
