using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OrgFlow.Application.Positions.Queries;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Positions.Handlers
{
    public class GetAllPositionsQueryHandler
     : IRequestHandler<GetAllPositionsQuery, IReadOnlyList<Position>>
    {
        private readonly IPositionRepository _repo;
        private readonly ILogger<GetAllPositionsQueryHandler> _logger;

        public GetAllPositionsQueryHandler(
            IPositionRepository repo,
            ILogger<GetAllPositionsQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Position>> Handle(
            GetAllPositionsQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all positions");
            return await _repo.GetAllAsync();
        }
    }
}
