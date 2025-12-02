using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OegFlow.Domain.Enums;
using OrgFlow.Application.Positions.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.Positions.Handlers
{
    public class CreatePositionCommandHandler
     : IRequestHandler<CreatePositionCommand, Position>
    {
        private readonly IPositionRepository _repo;
        private readonly ILogger<CreatePositionCommandHandler> _logger;

        public CreatePositionCommandHandler(
            IPositionRepository repo,
            ILogger<CreatePositionCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Position> Handle(
            CreatePositionCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Position name is required.");

            if (dto.SeniorityLevelId == null)
                throw new ArgumentException("Seniority level is required.");

            if (dto.DefaultVacationDays <= 0)
                throw new ArgumentException("Default vacation days must be greater than zero.");

            var position = new Position
            {
                Name = dto.Name,
                SeniorityLevel = (SeniorityLevel)dto.SeniorityLevelId,
                DefaultVacationDays = dto.DefaultVacationDays,
                IsActive = true
            };

            await _repo.AddAsync(position);

            _logger.LogInformation("Position {Id} created ({Name})", position.Id, position.Name);

            return position;
        }
    }
}
