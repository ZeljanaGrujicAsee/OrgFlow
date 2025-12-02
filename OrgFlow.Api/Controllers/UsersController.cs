using MediatR;
using Microsoft.AspNetCore.Mvc;
using OegFlow.Domain.DTOs;
using OrgFlow.Application.Users.Commands;
using OrgFlow.Application.Users.Queries;

namespace OrgFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/users
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        // GET api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));
            if (user == null) return NotFound();
            return Ok(user);
        }

     

        // GET api/users/by-department/{deptId}
        [HttpGet("by-department/{departmentId}")]
        public async Task<IActionResult> GetByDepartment(int departmentId)
        {
            var users = await _mediator.Send(new GetUsersByDepartmentQuery(departmentId));
            return Ok(users);
        }

        // GET api/users/by-team/{teamId}
        [HttpGet("by-team/{teamId}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            var users = await _mediator.Send(new GetUsersByTeamQuery(teamId));
            return Ok(users);
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto dto)
        {
            var created = await _mediator.Send(new CreateUserCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUserDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in URL and body must match.");

            var updated = await _mediator.Send(new UpdateUserCommand(dto));
            return Ok(updated);
        }

        // DELETE api/users/{id}  -> soft delete (deactivate)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deactivate(int id)
        {
            await _mediator.Send(new DeactivateUserCommand(id));
            return NoContent();
        }
    }

}
