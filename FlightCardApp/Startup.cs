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
using System.Reflection;
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
            // add services

            // return third-party tool's class which is implemented IServiceProvider

            services.AddControllersWithViews().AddControllersAsServices();
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });


            //var builder = new ContainerBuilder();

            

            //IContainer container = builder.Build();


            //var handlers = container.Resolve <IEnumerable<IDomainEventHandler<FlightCanceled>>>();

            //foreach (var handler in handlers)
            //{
            //    handler.Handle(new FlightCanceled("1","cancel"));
            //}

            //return new AutofacServiceProvider(container);

        }

  

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    // Register your own things directly with Autofac here. Don't
        //    // call builder.Populate(), that happens in AutofacServiceProviderFactory
        //    // for you.
        //    builder.RegisterModule(new RegisterModule());
        //}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            //AutofacContainer = app.ApplicationServices.GetAutofacRoot();

           

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
