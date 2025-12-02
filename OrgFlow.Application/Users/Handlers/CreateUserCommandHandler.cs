using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.Users.Commands;
using OrgFlow.Domain.Entities;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Users.Handlers
{
    public class CreateUserCommandHandler
     : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _repo;
        private readonly ILogger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(
            IUserRepository repo,
            ILogger<CreateUserCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<User> Handle(
            CreateUserCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (string.IsNullOrWhiteSpace(dto.Email))
                throw new ArgumentException("Email is required.");

            var user = new User
            {
                DepartmentId = dto.DepartmentId,
                TeamId = dto.TeamId,
                PositionId = dto.PositionId,
                OfficeLocationId = dto.OfficeLocationId,
                ManagerId = dto.ManagerId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                IsActive = true
            };

            await _repo.AddAsync(user);
            _logger.LogInformation("User {Id} created", user.Id);

            return user;
        }
    }
}
