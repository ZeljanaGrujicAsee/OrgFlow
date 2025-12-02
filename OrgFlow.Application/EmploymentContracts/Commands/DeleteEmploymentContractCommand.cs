using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OrgFlow.Application.EmploymentContracts.Commands
{
    public record DeleteEmploymentContractCommand(int Id) : IRequest;

}
