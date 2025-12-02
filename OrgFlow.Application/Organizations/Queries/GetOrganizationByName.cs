using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrgFlow.Domain.Entites;

namespace OrgFlow.Application.Organizations.Queries
{
    public record GetOrganizationByNameQuery(string Name) : IRequest<Organization?>;

}
