using Autofac;
using FlightCardApp.libs.core;
using FlightCardApp.libs.domain;
using FlightCardApp.libs.infrasturcture;
using FlightCardApp.libs.persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FlightCardApp
{
    public class RegisterModule: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Other Lifetime
            // Transient
            //builder.RegisterType<EmployeeService>().As<IEmployeeService>()
            //    .InstancePerDependency();

            //// Scoped

            //      builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerLifetimeScope();


            //// Singleton
            //builder.RegisterType<EmployeeService>().As<IEmployeeService>()
            //    .SingleInstance();

            // Udi Dahan yöntemi eski yönetem static sınıf üzerine kurulu, buradaki örnek ise jimmy bogard domain event pattern yapısını bu uygulama da kullandık.

            builder.RegisterType<AutofacDomainEventDispatcher>().As<IDomainEventDispatcher>().SingleInstance();


            // service scaning 
            // Assembly.GetExecutingAssembly() current projedeki
            // Assembly assembly = Assembly.LoadFrom("MyNice.dll"); katmanlı mimaride okuma şekli
            // (typeof(Startup));

            // Assembly.GetAssembly(typeof(IDomainEvent)); bu katmandaki assembly load et demek katmanlı mimari için kullanabiliriz.

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).AsClosedTypesOf(typeof(IDomainEventHandler<>));



            //builder.RegisterType<ConvertOpenTicketHandler>().As<IDomainEventHandler<FlightCanceled>>().InstancePerLifetimeScope();
            //builder.RegisterType<FlightCanceledNotificationHandler>().As<IDomainEventHandler<FlightCanceled>>().InstancePerLifetimeScope();

            builder.RegisterType<EFFlightPlaningRepository>().As<IFlighPlaningRepository>();
            builder.RegisterType<EFFlightTicketRepository>().As<IFlightTicketRepository>();


            //builder.RegisterGeneric(typeof(EFBaseRepository<>))
            //.As(typeof(IRepository<>)).InstancePerLifetimeScope();














        }
    }
}
