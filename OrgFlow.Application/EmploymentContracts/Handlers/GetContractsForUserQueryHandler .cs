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
    public class GetContractsForUserQueryHandler
     : IRequestHandler<GetContractsForUserQuery, IReadOnlyList<EmploymentContract>>
    {
        private readonly IEmploymentContractRepository _repo;
        private readonly ILogger<GetContractsForUserQueryHandler> _logger;

        public GetContractsForUserQueryHandler(
            IEmploymentContractRepository repo,
            ILogger<GetContractsForUserQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<EmploymentContract>> Handle(
            GetContractsForUserQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching employment contracts for user {UserId}", request.UserId);
            return await _repo.GetByUserIdAsync(request.UserId);
        }
    }
}
