using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrgFlow.Domain.Entities;

namespace OrgFlow.Application.Users.Queries
{

    public record GetUsersByTeamQuery(int TeamId)
        : IRequest<IReadOnlyList<User>>;
}
