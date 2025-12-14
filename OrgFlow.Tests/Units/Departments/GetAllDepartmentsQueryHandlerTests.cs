using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OegFlow.Domain.Entities;
using OrgFlow.Application.Departments.Handlers;
using OrgFlow.Application.Departments.Queries;
using OrgFlow.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Tests.Units.Departments
{
    public class GetAllDepartmentsQueryHandlerTests
    {
        // Fake (lažni) repozitorijum koji glumi bazu.
        // Koristimo ga da handler testiramo BEZ prave baze.
        private readonly Mock<IDepartmentRepository> _repo;

        // Fake logger, da ne bismo pisali logove tokom testova.
        private readonly Mock<ILogger<GetAllDepartmentsQueryHandler>> _logger;

        // Handler koji testiramo. Ovo je klasa koja sadrži logiku za GET all upit.
        private readonly GetAllDepartmentsQueryHandler _handler;


        // Konstruktor test klase – izvršava se pre svakog testa.
        public GetAllDepartmentsQueryHandlerTests()
        {
            // Pravljenje lažnog repozitorijuma.
            _repo = new Mock<IDepartmentRepository>();

            // Pravljenje lažnog loggera.
            _logger = new Mock<ILogger<GetAllDepartmentsQueryHandler>>();

            // Handler dobija lažne zavisnosti i tako radimo izolovano testiranje logike.
            _handler = new GetAllDepartmentsQueryHandler(_repo.Object, _logger.Object);
        }


        // -------------------------------------------------------------
        //                        TEST SCENARIO
        // -------------------------------------------------------------
        // Cilj:
        // Proveriti da handler ispravno vraća listu departmana
        // koju dobija iz repozitorijuma.
        //
        // Drugim rečima:
        // - Ako repozitorijum vrati 3 departmana,
        //   handler mora da vrati TAČNO ta 3 departmana.
        // -------------------------------------------------------------

        [Fact] // Oznaka da je ovo jedan test
        public async Task Handle_ShouldReturnDepartments()
        {
            // Faker kreira lažne, ali realistične modele departmana.
            // Automatski popunjava Name za svaku instancu.
            var faker = new Faker<Department>()
                .RuleFor(d => d.Name, f => f.Commerce.Department());

            // Pravljenje liste od 3 slučajna departmana.
            var departments = faker.Generate(3);

            // -------------------------------------------------------------
            // Podešavamo ponašanje fake repozitorijuma:
            // Kada neko pozove GetAllAsync, on treba da vrati našu listu.
            // -------------------------------------------------------------
            _repo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
                 .ReturnsAsync(departments);

            // Pozivamo handler → simuliramo GET all upit.
            var result = await _handler.Handle(new GetAllDepartmentsQuery(), default);

            // -------------------------------------------------------------
            // Asertacija (provera):
            // Da li handler vraća TAČNO 3 departmana kao što smo očekivali?
            // -------------------------------------------------------------
            result.Should().HaveCount(3);
        }
    }

}
