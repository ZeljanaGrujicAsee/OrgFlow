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
    public class GetDepartmentByIdQueryHandlerTests
    {
        // Lažna verzija IDepartmentRepository interfejsa.
        // U testovima želimo da sami kontrolišemo šta repozitorijum vraća,
        // zato pravimo "mock" umesto prave implementacije.
        private readonly Mock<IDepartmentRepository> _repo;

        // Fake logger - čisto da handler ne očekuje pravi logger.
        private readonly Mock<ILogger<GetDepartmentByIdQueryHandler>> _logger;

        // Ovo je handler koji testiramo — realna logika iz Application sloja.
        private readonly GetDepartmentByIdQueryHandler _handler;


        // Konstruktor test klase — pravi sve mock objekte za svaki test.
        public GetDepartmentByIdQueryHandlerTests()
        {
            _repo = new Mock<IDepartmentRepository>();
            _logger = new Mock<ILogger<GetDepartmentByIdQueryHandler>>();

            // Handler dobija lažni repozitorijum i lažni logger.
            // Ovo nam omogućava da testiramo SAMO logiku handlera,
            // bez baze i spoljnjih faktora.
            _handler = new GetDepartmentByIdQueryHandler(_repo.Object, _logger.Object);
        }


        // --------------------------------------------------------------------
        // TEST 1: Handler treba da vrati department ako ga repozitorijum pronađe.
        // --------------------------------------------------------------------
        [Fact]
        public async Task Handle_ShouldReturnDepartment_WhenExists()
        {
            // Pravimo testni department koji bi baza inače vratila.
            var department = new Department { Id = 10, Name = "IT" };

            // Kažemo mock repozitorijumu:
            // "Ako neko pozove GetByIdAsync sa ID = 10, ti vrati ovaj department."
            _repo.Setup(r => r.GetByIdAsync(10, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(department);

            // Handler poziv - kao da pravi korisnik traži department sa ID=10.
            var result = await _handler.Handle(new GetDepartmentByIdQuery(10), default);

            // Očekujemo da NIJE null — znači pronađen je.
            result.Should().NotBeNull();

            // Proveravamo da li je ID isti kao što smo zadali.
            result!.Id.Should().Be(10);
        }


        // --------------------------------------------------------------------
        // TEST 2: Handler treba da vrati NULL ako department NE postoji.
        // --------------------------------------------------------------------
        [Fact]
        public async Task Handle_ShouldReturnNull_WhenNotFound()
        {
            // Kažemo mock repozitorijumu:
            // "Ako neko traži ID 99 — vrati null (znači da ne postoji)."
            _repo.Setup(r => r.GetByIdAsync(99, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((Department?)null);

            // Handler poziv za ID 99.
            var result = await _handler.Handle(new GetDepartmentByIdQuery(99), default);

            // Očekujemo null — jer department ne postoji u bazi.
            result.Should().BeNull();
        }
    }
}
