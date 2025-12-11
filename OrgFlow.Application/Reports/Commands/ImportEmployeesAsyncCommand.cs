using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.Reports.Commands
{
    public record ImportEmployeesAsyncCommand(IFormFile file) : IRequest<string>
    {

    }
}
