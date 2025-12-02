using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OrgFlow.Application.Teams.Commands
{
    public record DeleteTeamCommand(int Id) : IRequest;

}
