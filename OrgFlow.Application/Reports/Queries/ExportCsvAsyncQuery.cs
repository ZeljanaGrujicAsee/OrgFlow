using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgFlow.Application.Reports.Queries
{
    public record ExportCsvAsyncQuery(string separator) : IRequest<string>
    {
    }
}
