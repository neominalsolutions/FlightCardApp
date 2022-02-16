using Autofac;
using Autofac.Extensions.DependencyInjection;
using FlightCardApp.libs.core;
using FlightCardApp.libs.domain;
using FlightCardApp.libs.infrasturcture;
using FlightCardApp.libs.persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightCardApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // IOC Container

            //services.AddScope

            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //services.AddTransient<IDomainEventHandler<FlightCanceled>, ConvertOpenTicketHandler>();
            //services.AddTransient<IDomainEventHandler<FlightCanceled>, FlightCanceledNotificationHandler>();

            //services.AddTransient<IDomainEventDispatcher, NetCoreEventDispatcher>();
            //services.AddScoped<IFlighPlaningRepository, EFFlightPlaningRepository>();
            //services.AddScoped<IFlightTicketRepository, EFFlightTicketRepository>();
            // Dispatcherlar performans amaçlý uygulama genelinde 1 kez instance alýnýr.


            // startup dosyamýzda sitemde event fýrlattýktan sonra tetiklenecek olan handlerlarý tanýmlýyoruz. // AddTransient yapalým



            //var container = new StandardKernel();
            //container.Bind<IDomainEventDispatcher, NinjectDomainEventDispatcher>();
            //container.Bind<IDomainEventHandler<FlightCanceled>>().To<ConvertOpenTicketHandler>();
            //container.Bind<IDomainEventHandler<FlightCanceled>>().To<FlightCanceledNotificationHandler>();
            //container.Bind<IFlighPlaningRepository, EFFlightPlaningRepository>();
            //container.Bind<IFlightTicketRepository, EFFlightTicketRepository>();
            //container.Bind<AppDbContext>();


            var containerBuilder = new ContainerBuilder();
            //Register your own services within Autofac
            containerBuilder.RegisterType<ConvertOpenTicketHandler>().As<IDomainEventHandler<FlightCanceled>>();
            containerBuilder.RegisterType<FlightCanceledNotificationHandler>().As<IDomainEventHandler<FlightCanceled>>();
            containerBuilder.RegisterType<EFFlightPlaningRepository>().As<IFlighPlaningRepository>();
            containerBuilder.RegisterType<EFFlightTicketRepository>().As<IFlightTicketRepository>();
            containerBuilder.RegisterType<NinjectDomainEventDispatcher>().As<IDomainEventDispatcher>();
            //Put the framework services into Autofac
            containerBuilder.Populate(services);

            //Build and return the Autofac collection
            var container = containerBuilder.Build();
          





            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
