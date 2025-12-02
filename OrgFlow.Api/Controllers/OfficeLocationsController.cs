using MediatR;
using Microsoft.AspNetCore.Mvc;
using OegFlow.Domain.DTOs;
using OrgFlow.Application.OfficeLocations.Commands;
using OrgFlow.Application.OfficeLocations.Queries;

namespace OrgFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfficeLocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeLocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/officelocations
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mediator.Send(new GetAllOfficeLocationsQuery());
            return Ok(items);
        }

        // GET: api/officelocations/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetOfficeLocationByIdQuery(id));
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // GET: api/officelocations/by-organization/3
        [HttpGet("by-organization/{organizationId}")]
        public async Task<IActionResult> GetByOrganization(int organizationId)
        {
            var items = await _mediator.Send(new GetOfficeLocationsByOrganizationQuery(organizationId));
            return Ok(items);
        }

        // POST: api/officelocations
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOfficeLocationDto dto)
        {
            var created = await _mediator.Send(new CreateOfficeLocationCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/officelocations/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOfficeLocationDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in URL and body must match.");

            var updated = await _mediator.Send(new UpdateOfficeLocationCommand(dto));
            return Ok(updated);
        }

        // DELETE: api/officelocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteOfficeLocationCommand(id));
            return NoContent();
        }
    }
}
