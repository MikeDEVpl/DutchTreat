using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build(); //Stworzenie buildera

            SeedDb(host);

            host.Run(); //dopiero tutaj run buildera
        }

        private static void SeedDb(IHost host)
        {
            //Tworzy scope poniewa¿ context ze startupu musi miec swoj scope
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //Reczna konfiguracja do nauki.
                .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            builder.Sources.Clear(); //usuwa domyslne konfiguracje

            //Builder buduje konfiguracje z kilku zróde³
            //Ostatni na liœcie nadpisuje wy¿sze gdy s¹ te same wartoœci
            builder.AddJsonFile("config.json", false, true)
                .AddXmlFile("config.xml", true) //opcjonalny config
                .AddEnvironmentVariables();
        }
    }
}
