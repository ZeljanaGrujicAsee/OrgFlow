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
    public class UpdateDepartmentCommandHandlerTests
    {
        // Fake repozitorijum – glumi pristup bazi.
        // Koristimo ga da handler možemo da testiramo BEZ prave baze.
        private readonly Mock<IDepartmentRepository> _repo;

        // Fake logger – samo da handler ima sve zavisnosti.
        private readonly Mock<ILogger<UpdateDepartmentCommandHandler>> _logger;

        // Handler čiju logiku testiramo.
        private readonly UpdateDepartmentCommandHandler _handler;


        // Konstruktor test klase — priprema fake objekte pre svakog testa.
        public UpdateDepartmentCommandHandlerTests()
        {
            _repo = new Mock<IDepartmentRepository>();
            _logger = new Mock<ILogger<UpdateDepartmentCommandHandler>>();

            // Handler dobija fejkovane zavisnosti.
            // Ovo omogućava da testiramo SAMO njegovu logiku — bez baze.
            _handler = new UpdateDepartmentCommandHandler(_repo.Object, _logger.Object);
        }


        // ---------------------------------------------------------------------
        // TEST 1: Handler treba da baci grešku ako department NE postoji
        // ---------------------------------------------------------------------
        [Fact]
        public async Task Handle_ShouldThrow_WhenDepartmentNotFound()
        {
            // DTO koji simulira podatke koje korisnik šalje za update.
            var dto = new UpdateDepartmentDto { Id = 5, Name = "IT", OrganizationId = 1 };

            var command = new UpdateDepartmentCommand(dto);

            // Kažemo fake repozitorijumu:
            // "Ako se traži ID=5, vrati null (kao da ne postoji u bazi)".
            _repo.Setup(r => r.GetByIdAsync(5, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Department?)null);

            // Handler treba da baci KeyNotFoundException.
            // Invoking → pokreće handler unutar FluentAssertions ekstenzije.
            await _handler.Invoking(h => h.Handle(command, default))
                .Should()
                .ThrowAsync<KeyNotFoundException>();
        }


        // ---------------------------------------------------------------------
        // TEST 2: Handler treba da uspešno update-uje department
        // ---------------------------------------------------------------------
        [Fact]
        public async Task Handle_ShouldUpdateDepartment()
        {
            // Postojeći department u bazi
            var existing = new Department
            {
                Id = 5,
                Name = "Old",
                OrganizationId = 1
            };

            // DTO koji simulira nove vrednosti koje korisnik šalje
            var dto = new UpdateDepartmentDto
            {
                Id = 5,
                Name = "New IT",
                OrganizationId = 1,
                Description = "Updated",
                IsActive = true
            };

            // Fake repozitorijum treba da "pronađe" postojeći department
            _repo.Setup(r => r.GetByIdAsync(5, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(existing);

            // Fake repozitorijum simulira uspešan Update
            _repo.Setup(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            var handler = new UpdateDepartmentCommandHandler(_repo.Object, _logger.Object);

            // Handler izvršava update
            var result = await handler.Handle(new UpdateDepartmentCommand(dto), default);

            // Provere: da li su polja ažurirana ispravno?
            result.Name.Should().Be("New IT");
            result.Description.Should().Be("Updated");

            // Provera: da li je repozitorijum stvarno pozvao UpdateAsync TAČNO jednom?
            _repo.Verify(r => r.UpdateAsync(existing, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
