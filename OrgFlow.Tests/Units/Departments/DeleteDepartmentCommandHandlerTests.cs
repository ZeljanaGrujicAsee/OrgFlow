using Microsoft.Extensions.Logging;
using Moq;
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
    public class DeleteDepartmentCommandHandlerTests
    {
        // Ovo je "lažni" repozitorijum (Mock) koji koristimo umesto prave baze.
        // Ideja: ne želimo da handler zapravo briše iz baze tokom testiranja.
        private readonly Mock<IDepartmentRepository> _repo;

        // I logger se lažira (fake). U testovima ne želimo pravi output u log.
        private readonly Mock<ILogger<DeleteDepartmentCommandHandler>> _logger;

        // Handler koji testiramo – on sadrži logiku brisanja.
        private readonly DeleteDepartmentCommandHandler _handler;


        // Konstruktor test klase: izvodi se pre svakog testa.
        public DeleteDepartmentCommandHandlerTests()
        {
            // Pravimo "fejk" repozitorijum.
            _repo = new Mock<IDepartmentRepository>();

            // Pravimo "fejk" logger.
            _logger = new Mock<ILogger<DeleteDepartmentCommandHandler>>();

            // Pravi handler i ubrizgava mu fake repozitorijum i fake logger.
            // Time testiramo SAMO logiku handlera — bez baze i bez pravog logovanja.
            _handler = new DeleteDepartmentCommandHandler(_repo.Object, _logger.Object);
        }


        // --------------------------------------------------------------
        // TEST: Da li handler zaista poziva repozitorijum da obriše entitet?
        // --------------------------------------------------------------
        [Fact] // Ovaj atribut označava da je metoda test.
        public async Task Handle_ShouldDeleteDepartment()
        {
            // Ovo je komanda koju handler prima.
            // U njemu je ID departmana koji želimo da obrišemo (npr. ID = 5).
            var command = new DeleteDepartmentCommand(5);

            // ------------------------------------------------------------
            // Podešavamo očekivano ponašanje fake repozitorijuma:
            //
            // Kada se pozove DeleteAsync(5, ...),
            // lažni repozitorijum samo "glumi" da je sve prošlo OK.
            //
            // Returns(Task.CompletedTask) znači: operacija uspešno završena.
            // ------------------------------------------------------------
            _repo.Setup(r => r.DeleteAsync(5, It.IsAny<CancellationToken>()))
                 .Returns(Task.CompletedTask);

            // Pozivamo handler metodu Handle, kao da je stvarno API pozvao brisanje.
            await _handler.Handle(command, default);

            // ------------------------------------------------------------
            // Ovaj deo je najvažniji:
            // Proveravamo da li je repozitorijum zaista pozvan TAČNO JEDNOM sa ID = 5.
            //
            // Drugim rečima:
            //  - Handler mora da pozove DeleteAsync
            //  - Mora da prosledi pravi ID
            //  - Ne sme da preskoči brisanje
            // ------------------------------------------------------------
            _repo.Verify(r => r.DeleteAsync(5, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
