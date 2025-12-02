using MediatR;
using Microsoft.AspNetCore.Mvc;
using OegFlow.Domain.DTOs;
using OrgFlow.Application.Teams.Commands;
using OrgFlow.Application.Teams.Queries;
using OrgFlow.Application.Teams;

namespace OrgFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/teams
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _mediator.Send(new GetAllTeamsQuery());
            return Ok(teams);
        }

        // GET: api/teams/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var team = await _mediator.Send(new GetTeamByIdQuery(id));
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        // GET: api/teams/by-department/3
        [HttpGet("by-department/{departmentId}")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var teams = await _mediator.Send(new GetTeamsForDepartmentQuery(departmentId));
            return Ok(teams);
        }

        // POST: api/teams
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTeamDto dto)
        {
            var created = await _mediator.Send(new CreateTeamCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/teams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in URL and body must match.");

            var updated = await _mediator.Send(new UpdateTeamCommand(dto));
            return Ok(updated);
        }

        // DELETE: api/teams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteTeamCommand(id));
            return NoContent();
        }
    }
}
