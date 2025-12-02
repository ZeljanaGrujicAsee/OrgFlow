using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OegFlow.Domain.DTOs;
using OrgFlow.Domain.Entities;

namespace OrgFlow.Application.Users.Commands
{
    public record CreateUserCommand(CreateUserDto Dto) : IRequest<User>;

}
