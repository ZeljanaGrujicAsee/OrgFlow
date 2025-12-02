using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OrgFlow.Application.Organizations.Commands
{
    public record DeactivateOrganizationCommand(int Id) : IRequest;

}
