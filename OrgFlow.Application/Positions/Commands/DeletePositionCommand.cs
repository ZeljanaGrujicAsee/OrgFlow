using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Positions.Commands
{
    public record DeletePositionCommand(int Id) : IRequest;

}
