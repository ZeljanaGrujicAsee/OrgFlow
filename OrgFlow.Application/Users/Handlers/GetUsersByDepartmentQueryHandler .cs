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
    public class GetUsersByDepartmentQueryHandler
       : IRequestHandler<GetUsersByDepartmentQuery, IReadOnlyList<User>>
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<GetUsersByDepartmentQueryHandler> _logger;

        public GetUsersByDepartmentQueryHandler(
            IUserRepository repo,
            ILogger<GetUsersByDepartmentQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<IReadOnlyList<User>> Handle(
            GetUsersByDepartmentQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching users for department {DeptId}", request.DepartmentId);
            return await _repo.GetByDepartmentAsync(request.DepartmentId);
        }
    }

}
