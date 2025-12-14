using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrgFlow.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using OrgFlow.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
namespace OrgFlow.Tests.Integration.Setup
{

        // Klasa koja pravi "lažnu" (test) verziju naše kompletne API aplikacije.
        // Ova klasa omogućava da Integration testovi pokreću API bez pravog servera.
        // WebApplicationFactory<Program> znači:
        //   - pokreni naš Program.cs kao da startuje aplikaciju
        //   - ALI dozvoli nam da promenimo konfiguraciju samo za testiranje.
        public class OrgFlowWebAppFactory : WebApplicationFactory<Program>
        {
            // Ovaj metod se automatski poziva pre nego što test okruženje pokrene aplikaciju.
            // Omogućava da promenimo DI container, servise, bazu itd.
            protected override void ConfigureWebHost(IWebHostBuilder builder)
            {
                builder.ConfigureServices(services =>
                {
                    // ---------------------------------------------
                    // 1) Uklanjamo "pravu" bazu podataka iz aplikacije
                    // ---------------------------------------------
                    //
                    // U Program.cs se verovatno registruje SQL Server ili neka realna baza.
                    // Mi NE ŽELIMO da integration testovi diraju stvarnu bazu.
                    // Zato je tražimo u listi servisa i uklanjamo je.
                    //
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<OrgFlowDbContext>));

                    // Ako pronađemo postojeću konfiguraciju baze — brišemo je.
                    if (descriptor != null)
                        services.Remove(descriptor);

                    // ---------------------------------------------
                    // 2) Dodajemo novu — InMemory bazu
                    // ---------------------------------------------
                    //
                    // Ovo kreira potpuno čistu bazu svaki put kada pokreneš test.
                    // Testovi NE dele podatke, NE utiču na pravu bazu
                    // i NE zavise jedni od drugih.
                    //
                    // "OrgFlowTestDb" je samo ime (moglo je biti bilo šta).
                    //
                    services.AddDbContext<OrgFlowDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("OrgFlowTestDb");
                        // UseInMemoryDatabase = .NET pravi RAM bazu umesto SQL Server-a.
                    });

                    // Od ovog trenutka sve što API radi tokom testiranja:
                    // - dodaje departmane
                    // - ažurira
                    // - briše
                    // - čita iz baze
                    // radi isključivo u memorijskoj bazi specifično za ovaj test.
                });
            }
        }
   }

