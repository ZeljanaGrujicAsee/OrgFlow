using Bogus;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using OegFlow.Domain.DTOs;
using OegFlow.Domain.Entities;
using OrgFlow.Application.Departments.Commands;
using OrgFlow.Application.Departments.Handlers;
using OrgFlow.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Tests.Units.Departments
{
    public class CreateDepartmentCommandHandlerTests
    {
        // Pravljenje "lažnih" (fake) objekata umesto pravih implementacija.
        // Mock IDepartmentRepository nam omogućava da testiramo handler BEZ stvarne baze.
        private readonly Mock<IDepartmentRepository> _repo;

        // Logger se takođe "fejkuje" jer u testovima ne želimo pravi logging.
        private readonly Mock<ILogger<CreateDepartmentCommandHandler>> _logger;

        // Handler koji testiramo.
        // Ovo je klasa čiji se behavior proverava u ovim testovima.
        private readonly CreateDepartmentCommandHandler _handler;


        // Konstruktor test klase — pokreće se pre svakog testa.
        public CreateDepartmentCommandHandlerTests()
        {
            // Pravi instance lažnog repozitorijuma.
            _repo = new Mock<IDepartmentRepository>();

            // Pravi instance lažnog loggera.
            _logger = new Mock<ILogger<CreateDepartmentCommandHandler>>();

            // Pravimo handler sa lažnim zavisnostima.
            // Ovo nam omogućava da testiramo SAMO logiku handlera.
            _handler = new CreateDepartmentCommandHandler(_repo.Object, _logger.Object);
        }


        // ------------------------------------------------------------
        // TEST #1: Provera validacije — da li baca grešku za prazno ime?
        // ------------------------------------------------------------
        [Fact] // Ova oznaka govori da je ovo samostalni test.
        public async Task Handle_ShouldThrow_WhenNameIsEmpty()
        {
            // Pravimo DTO sa PRAZNIM nazivom — ovo bi trebalo da bude greška.
            var dto = new CreateDepartmentDto { Name = "", OrganizationId = 1 };

            // Pretvaramo DTO u CQRS komandu za handler.
            var command = new CreateDepartmentCommand(dto);

            // Očekujemo da handler baci ArgumentException kada obradi komandu.
            await _handler.Invoking(h => h.Handle(command, default))
                .Should()                       // koristimo FluentAssertions
                .ThrowAsync<ArgumentException>() // očekujemo exception tip
                .WithMessage("*required*");      // očekujemo da poruka SADRŽI reč "required"
        }


        // ------------------------------------------------------------
        // TEST #2: Da li handler uspešno kreira Department objekat?
        // ------------------------------------------------------------
        [Fact]
        public async Task Handle_ShouldCreateDepartment()
        {
            // Bogus faker pravi nasumične, ali realistične podatke.
            var faker = new Faker();

            // Kreiramo DTO sa random vrednostima.
            var dto = new CreateDepartmentDto
            {
                Name = faker.Commerce.Department(),   // npr. "Electronics"
                Description = faker.Lorem.Sentence(), // stvarna rečenica
                OrganizationId = 1
            };

            // Komanda prema handleru
            var command = new CreateDepartmentCommand(dto);

            // ------------------------------------------------------------
            // Podešavamo mock repozitorijum:
            // Bilo koje AddAsync pozivanje treba da se "pretvara" da radi.
            // Ne koristimo stvarnu bazu — zato "Returns(Task.CompletedTask)".
            // ------------------------------------------------------------
            _repo.Setup(r => r.AddAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            // Pozivamo handler i čekamo rezultat.
            var result = await _handler.Handle(command, default);

            // ------------------------------------------------------------
            // Proveravamo rezultat — FluentAssertions omogućava lep način pisanja asertacija:
            // ------------------------------------------------------------

            result.Should().NotBeNull();                 // Handler mora vratiti objekat.
            result.Name.Should().Be(dto.Name);           // Atributi moraju biti isti kao u DTO-u.
            result.OrganizationId.Should().Be(dto.OrganizationId);
            result.Description.Should().Be(dto.Description);

            // ------------------------------------------------------------
            // Proveravamo da li je repozitorijum zaista pozvan tačno jednom.
            // Ovo garantuje da handler pokušava da sačuva objekat.
            // ------------------------------------------------------------
            _repo.Verify(
                r => r.AddAsync(It.IsAny<Department>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
