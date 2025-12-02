using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OegFlow.Domain.Entities;
using OrgFlow.Application.EmploymentContracts.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.EmploymentContracts.Handlers
{
    public class UpdateEmploymentContractCommandHandler
       : IRequestHandler<UpdateEmploymentContractCommand, EmploymentContract>
    {
        private readonly IEmploymentContractRepository _repo;
        private readonly ILogger<UpdateEmploymentContractCommandHandler> _logger;

        public UpdateEmploymentContractCommandHandler(
            IEmploymentContractRepository repo,
            ILogger<UpdateEmploymentContractCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<EmploymentContract> Handle(
            UpdateEmploymentContractCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            var existing = await _repo.GetByIdAsync(dto.Id)
                ?? throw new KeyNotFoundException($"EmploymentContract {dto.Id} not found.");

            existing.UserId = dto.UserId;
            existing.ContractType = dto.ContractType;
            existing.StartDate = dto.StartDate;
            existing.EndDate = dto.EndDate;
            existing.IsCurrent = dto.IsCurrent;
            existing.SalaryGross = dto.SalaryGross;
            existing.Currency = dto.Currency;

            await _repo.UpdateAsync(existing);

            if (existing.IsCurrent)
            {
                await _repo.SetCurrentContractAsync(existing.UserId, existing);
            }

            _logger.LogInformation("Employment contract {Id} updated", existing.Id);

            return existing;
        }
    }
}
