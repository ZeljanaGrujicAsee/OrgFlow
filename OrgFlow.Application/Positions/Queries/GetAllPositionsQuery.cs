using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OegFlow.Domain.Entities;

namespace OrgFlow.Application.Positions.Queries
{
    public record GetAllPositionsQuery : IRequest<IReadOnlyList<Position>>;

}
