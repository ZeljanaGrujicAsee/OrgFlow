using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OegFlow.Domain.Enums;
using OrgFlow.Application.Positions.Queries;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Positions.Handlers
{
    public class GetPositionsBySeniorityLevelQueryHandler
     : IRequestHandler<GetPositionsBySeniorityLevelQuery, IReadOnlyList<Position>>
    {
        private readonly IPositionRepository _repo;
        private readonly ILogger<GetPositionsBySeniorityLevelQueryHandler> _logger;

        public GetPositionsBySeniorityLevelQueryHandler(
            IPositionRepository repo,
            ILogger<GetPositionsBySeniorityLevelQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<Position>> Handle(
            GetPositionsBySeniorityLevelQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching positions for seniority level {Level}", request.SeniorityLevelId);

            return await _repo.GetBySeniorityLevelAsync((SeniorityLevel)request.SeniorityLevelId);
        }
    }
}
