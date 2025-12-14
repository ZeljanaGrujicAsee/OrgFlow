using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OegFlow.Domain;
using OegFlow.Domain.DTOs;
using OrgFlow.Application.Departments.Commands;
using OrgFlow.Application.Departments.Queries;

namespace OrgFlow.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class DepartmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UserContext _ctx;

        public DepartmentsController(IMediator mediator, UserContext ctx)
        {
            _mediator = mediator;
            _ctx = ctx;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var orgId = _ctx.OrganizationId;
            var deptId = _ctx.DepartmentId;
            var userId = _ctx.UserId;
            var role = _ctx.Role;
            var items = await _mediator.Send(new GetAllDepartmentsQuery());
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _mediator.Send(new GetDepartmentByIdQuery(id));
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto)
        {
            var created = await _mediator.Send(new CreateDepartmentCommand(dto));
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Id in URL and body must match.");

            var updated = await _mediator.Send(new UpdateDepartmentCommand(dto));
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteDepartmentCommand(id));
            return NoContent();
        }
    }
}
