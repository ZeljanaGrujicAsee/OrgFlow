using MediatR;
using Microsoft.AspNetCore.Mvc;
using OegFlow.Domain.DTOs;
using OrgFlow.Application.Positions.Commands;
using OrgFlow.Application.Positions.Queries;

namespace OrgFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PositionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/positions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _mediator.Send(new GetAllPositionsQuery());
            return Ok(items);
        }

        // GET: api/positions/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetPositionByIdQuery(id));
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // GET: api/positions/by-seniority/{level}
        [HttpGet("by-seniority/{level}")]
        public async Task<IActionResult> GetBySeniority(int levelId)
        {
            var items = await _mediator.Send(new GetPositionsBySeniorityLevelQuery(levelId));
            return Ok(items);
        }

        // POST: api/positions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePositionDto dto)
        {
            var created = await _mediator.Send(new CreatePositionCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/positions/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePositionDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in URL and body must match.");

            var updated = await _mediator.Send(new UpdatePositionCommand(dto));
            return Ok(updated);
        }

        // DELETE: api/positions/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeletePositionCommand(id));
            return NoContent();
        }
    }

}
