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
    public class GetPositionByIdQueryHandler
     : IRequestHandler<GetPositionByIdQuery, Position?>
    {
        private readonly IPositionRepository _repo;
        private readonly ILogger<GetPositionByIdQueryHandler> _logger;

        public GetPositionByIdQueryHandler(
            IPositionRepository repo,
            ILogger<GetPositionByIdQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Position?> Handle(
            GetPositionByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching position {Id}", request.Id);
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
