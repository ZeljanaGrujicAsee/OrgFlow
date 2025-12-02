using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Users.Queries;
using OrgFlow.Domain.Entities;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Users.Handlers
{
    public class GetUsersByTeamQueryHandler
     : IRequestHandler<GetUsersByTeamQuery, IReadOnlyList<User>>
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<GetUsersByTeamQueryHandler> _logger;

        public GetUsersByTeamQueryHandler(
            IUserRepository repo,
            ILogger<GetUsersByTeamQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<User>> Handle(
            GetUsersByTeamQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching users for team {TeamId}", request.TeamId);
            return await _repo.GetByTeamAsync(request.TeamId);
        }
    }
}
