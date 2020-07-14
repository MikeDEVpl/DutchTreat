using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace DutchTreat
{
    public class Startup
    {
        private IConfiguration _config { get; }

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DutchContext>(cfg => 
            {
                cfg.UseSqlServer(_config.GetConnectionString("DutchConnectionString"));
            });
           services.AddTransient<DutchSeeder>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly()); //dodanie automapera dla kazdego miejsca
            services.AddScoped<IDutchRepository, DutchRepository>();
            services.AddTransient<IMailService, NullMailService>();
            services.AddControllersWithViews()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0)
                .AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsEnvironment("Development"))
            { 
                app.UseDeveloperExceptionPage(); 
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.UseDefaultFiles(); // Powoduje ,¿e automatycznie wyœwietla index bez potrzeby wpisywania w pasku

            app.UseStaticFiles();  // Serwuje pliki z folderu wwwroot
            app.UseNodeModules();

            app.UseRouting(); //Dodaje ruting dla controller i views

            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Fallback",
                    "{controller}/{action}/{id?}",
                    new { controller = "App", action = "Index" });
            });


        }
    }
}
