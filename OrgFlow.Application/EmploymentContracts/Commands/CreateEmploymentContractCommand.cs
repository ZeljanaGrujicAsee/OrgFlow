using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using OegFlow.Domain.DTOs;
using OegFlow.Domain.Entities;

namespace OrgFlow.Application.EmploymentContracts.Commands
{

    public record CreateEmploymentContractCommand(CreateEmploymentContractDto Dto)
        : IRequest<EmploymentContract>;
}
