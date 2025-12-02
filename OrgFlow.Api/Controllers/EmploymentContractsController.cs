using MediatR;
using Microsoft.AspNetCore.Mvc;
using OegFlow.Domain.DTOs;
using OrgFlow.Application.EmploymentContracts.Commands;
using OrgFlow.Application.EmploymentContracts.Queries;

namespace OrgFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmploymentContractsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmploymentContractsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/employmentcontracts/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetForUser(int userId)
        {
            var contracts = await _mediator.Send(new GetContractsForUserQuery(userId));
            return Ok(contracts);
        }

        // GET: api/employmentcontracts/user/5/current
        [HttpGet("user/{userId}/current")]
        public async Task<IActionResult> GetCurrentForUser(int userId)
        {
            var contract = await _mediator.Send(new GetCurrentContractForUserQuery(userId));
            if (contract == null)
                return NotFound();

            return Ok(contract);
        }

        // GET: api/employmentcontracts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contract = await _mediator.Send(new GetEmploymentContractByIdQuery(id));
            if (contract == null)
                return NotFound();

            return Ok(contract);
        }

        // POST: api/employmentcontracts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmploymentContractDto dto)
        {
            var created = await _mediator.Send(new CreateEmploymentContractCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/employmentcontracts/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEmploymentContractDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");

            var updated = await _mediator.Send(new UpdateEmploymentContractCommand(dto));
            return Ok(updated);
        }

        // DELETE: api/employmentcontracts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteEmploymentContractCommand(id));
            return NoContent();
        }
    }
}
