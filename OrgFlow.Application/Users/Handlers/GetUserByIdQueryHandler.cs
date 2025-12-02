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
    public class GetUserByIdQueryHandler
    : IRequestHandler<GetUserByIdQuery, User?>
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(
            IUserRepository repo,
            ILogger<GetUserByIdQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<User?> Handle(
            GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching user {Id}", request.Id);
            return await _repo.GetByIdAsync(request.Id);
        }
    }
}
