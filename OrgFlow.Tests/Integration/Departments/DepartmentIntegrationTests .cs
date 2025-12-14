using FluentAssertions;
// Biblioteka koja omogućava da lepo i čitljivo provjerimo rezultate testova.
// Umesto komplikovanih if-ova, pišemo: rezultat.Should().Be(...)

using OegFlow.Domain.DTOs;
// DTO klase koje šaljemo API-ju kao request (CreateDepartmentDto, UpdateDepartmentDto, ...)

using OegFlow.Domain.Entities;
// Entiteti koje API vraća kao response (Department, Organization, ...)

using OrgFlow.Tests.Integration.Setup;
// Ovo je WebApplicationFactory – klasa koja pokreće naš API u test režimu.

using System.Net;
using System.Net.Http.Json;
// Omogućava da lako šaljemo JSON u HTTP requestovima i čitamo JSON odgovore.

// Namespace samo grupiše ove testove u logički folder.
namespace OrgFlow.Tests.Integration.Departments
{
    // Ova klasa testira PRAVU API komunikaciju – zato se zove *IntegrationTests*.
    // IClassFixture znači: "koristi jedan 'factory' objekat za sve testove".
    public class DepartmentIntegrationTests : IClassFixture<OrgFlowWebAppFactory>
    {
        private readonly HttpClient _client;
        // HttpClient glumi stvarnog korisnika koji poziva API.

        public DepartmentIntegrationTests(OrgFlowWebAppFactory factory)
        {
            // Factory kreira celu aplikaciju u memoriji i daje nam HTTP klijenta da je testiramo.
            _client = factory.CreateClient();
        }

        // -------------------------------------------------------------
        //                       CREATE TEST
        // -------------------------------------------------------------
        [Fact]
        public async Task CreateDepartment_ShouldReturnCreatedDepartment()
        {
            // Pravimo request telo — podatke kao da korisnik šalje POST na API.
            var request = new CreateDepartmentDto
            {
                Name = "Test Department",
                OrganizationId = 1,
                Description = "Integration test department"
            };

            // Šaljemo POST zahtev na pravi API endpoint.
            var response = await _client.PostAsJsonAsync("/api/departments", request);

            // Proveravamo da li je status kod = 201 Created.
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Pretvaramo JSON odgovor nazad u Department objekat.
            var dto = await response.Content.ReadFromJsonAsync<Department>();

            // Testiramo da odgovor nije null i da API vraća ono što smo poslali.
            dto.Should().NotBeNull();
            dto!.Name.Should().Be("Test Department");
            dto.OrganizationId.Should().Be(1);
        }

        // -------------------------------------------------------------
        //                       GET ALL TEST
        // -------------------------------------------------------------
        [Fact]
        public async Task GetAllDepartments_ShouldReturnList()
        {
            // Pozivamo GET /api/departments
            var response = await _client.GetAsync("/api/departments");

            // Očekujemo status 200 OK.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Odgovor treba da bude lista Department objekata.
            var result = await response.Content.ReadFromJsonAsync<List<Department>>();

            // Lista ne sme biti null.
            result.Should().NotBeNull();
        }

        // -------------------------------------------------------------
        //                      GET BY ID TEST
        // -------------------------------------------------------------
        [Fact]
        public async Task GetDepartmentById_ShouldReturnDepartment()
        {
            // 1) Prvo kreiramo Department da bismo imali ID za test.
            var create = new CreateDepartmentDto
            {
                Name = "HR",
                OrganizationId = 1
            };

            // Kačimo na API i stvaramo Department.
            var createRes = await _client.PostAsJsonAsync("/api/departments", create);

            // Čitamo objekat koji nam API vraća.
            var created = await createRes.Content.ReadFromJsonAsync<Department>();

            // 2) Sada tražimo GET po tom ID-u.
            var response = await _client.GetAsync($"/api/departments/{created!.Id}");

            // Očekujemo 200 OK.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Pretvaramo JSON u Department.
            var department = await response.Content.ReadFromJsonAsync<Department>();

            // Proveravamo da li smo dobili isti ID.
            department.Should().NotBeNull();
            department!.Id.Should().Be(created.Id);
        }

        // -------------------------------------------------------------
        //                        UPDATE TEST
        // -------------------------------------------------------------
        [Fact]
        public async Task UpdateDepartment_ShouldModifyDepartment()
        {
            // 1) Kreiramo Department koji ćemo kasnije update-ovati.
            var create = new CreateDepartmentDto
            {
                Name = "Finance",
                OrganizationId = 1
            };

            var createRes = await _client.PostAsJsonAsync("/api/departments", create);
            var existing = await createRes.Content.ReadFromJsonAsync<Department>();

            // 2) Pripremamo update request.
            var update = new UpdateDepartmentDto
            {
                Id = existing!.Id,           // ID mora da se poklapa sa URL-om
                Name = "Finance Updated",    // Novo ime
                OrganizationId = 1,
                Description = "Updated desc",
                IsActive = true
            };

            // Šaljemo PUT zahtev.
            var response = await _client.PutAsJsonAsync($"/api/departments/{update.Id}", update);

            // Očekujemo 200 OK.
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Čitamo izmenjeni objekat iz odgovora.
            var updated = await response.Content.ReadFromJsonAsync<Department>();

            // Proveravamo da li API zaista promenio ime.
            updated!.Name.Should().Be("Finance Updated");
        }

        // -------------------------------------------------------------
        //                        DELETE TEST
        // -------------------------------------------------------------
        [Fact]
        public async Task DeleteDepartment_ShouldReturnNoContent()
        {
            // 1) Kreiramo Department koji ćemo obrisati.
            var create = new CreateDepartmentDto
            {
                Name = "ToDelete",
                OrganizationId = 1
            };

            var createRes = await _client.PostAsJsonAsync("/api/departments", create);
            var created = await createRes.Content.ReadFromJsonAsync<Department>();

            // 2) Brišemo Department.
            var response = await _client.DeleteAsync($"/api/departments/{created!.Id}");

            // DELETE uspeva ako vrati 204 NoContent.
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }
    }
}