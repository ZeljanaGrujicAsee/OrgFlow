using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OrgFlow.Application.EmploymentContracts.Queries;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.EmploymentContracts.Handlers
{
    public class GetCurrentContractForUserQueryHandler
     : IRequestHandler<GetCurrentContractForUserQuery, EmploymentContract?>
    {
        private readonly IEmploymentContractRepository _repo;
        private readonly ILogger<GetCurrentContractForUserQueryHandler> _logger;

        public GetCurrentContractForUserQueryHandler(
            IEmploymentContractRepository repo,
            ILogger<GetCurrentContractForUserQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<EmploymentContract?> Handle(
            GetCurrentContractForUserQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching current employment contract for user {UserId}", request.UserId);
            return await _repo.GetCurrentForUserAsync(request.UserId);
        }
    }
}
