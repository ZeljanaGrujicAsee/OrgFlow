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
    public class CreateEmploymentContractCommandHandler
      : IRequestHandler<CreateEmploymentContractCommand, EmploymentContract>
    {
        private readonly IEmploymentContractRepository _repo;
        private readonly ILogger<CreateEmploymentContractCommandHandler> _logger;

        public CreateEmploymentContractCommandHandler(
            IEmploymentContractRepository repo,
            ILogger<CreateEmploymentContractCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<EmploymentContract> Handle(
            CreateEmploymentContractCommand request,
            CancellationToken cancellationToken)
        {
            var dto = request.Dto;

            if (string.IsNullOrWhiteSpace(dto.ContractType))
                throw new ArgumentException("ContractType is required.");

            var contract = new EmploymentContract
            {
                UserId = dto.UserId,
                ContractType = dto.ContractType,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsCurrent = dto.IsCurrent,
                SalaryGross = dto.SalaryGross,
                Currency = dto.Currency
            };

            await _repo.AddAsync(contract);

            if (contract.IsCurrent)
            {
                await _repo.SetCurrentContractAsync(contract.UserId, contract);
            }

            _logger.LogInformation(
                "Employment contract {Id} created for user {UserId}",
                contract.Id, contract.UserId);

            return contract;
        }
    }
}
