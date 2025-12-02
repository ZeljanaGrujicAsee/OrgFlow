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
    public class UpdatePositionCommandHandler
       : IRequestHandler<UpdatePositionCommand, Position>
    {
        private readonly IPositionRepository _repo;
        private readonly ILogger<UpdatePositionCommandHandler> _logger;

        public UpdatePositionCommandHandler(
            IPositionRepository repo,
            ILogger<UpdatePositionCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<Position> Handle(
            UpdatePositionCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existing = await _repo.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"Position {dto.Id} not found.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Position name is required.");

            if (dto.SeniorityLevelId == null)
                throw new ArgumentException("Seniority level is required.");

            if (dto.DefaultVacationDays <= 0)
                throw new ArgumentException("Default vacation days must be greater than zero.");

            existing.Name = dto.Name;
            existing.SeniorityLevel = (SeniorityLevel)dto.SeniorityLevelId;
            existing.DefaultVacationDays = dto.DefaultVacationDays;
            existing.IsActive = dto.IsActive;

            await _repo.UpdateAsync(existing);

            _logger.LogInformation("Position {Id} updated", existing.Id);

            return existing;
        }
    }
}
