using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OrgFlow.Api;
using OrgFlow.Domain.Entites;

namespace OrgFlow.Application.Organizations.Commands
{
    public record CreateOrganizationCommand(CreateOrganizationDto Dto)
     : IRequest<Organization>;
}
