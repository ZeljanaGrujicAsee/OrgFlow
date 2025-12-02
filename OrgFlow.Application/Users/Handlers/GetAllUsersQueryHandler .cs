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
    public class GetAllUsersQueryHandler
     : IRequestHandler<GetAllUsersQuery, IReadOnlyList<User>>
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<GetAllUsersQueryHandler> _logger;

        public GetAllUsersQueryHandler(
            IUserRepository repo,
            ILogger<GetAllUsersQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<User>> Handle(
            GetAllUsersQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all users");
            return await _repo.GetAllAsync();
        }
    }
}
