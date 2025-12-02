using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using OrgFlow.Application.OfficeLocations.Commands;
using OrgFlow.Infrastructure.Interfaces;

namespace OrgFlow.Application.OfficeLocations.Handlers
{
    public class DeleteOfficeLocationCommandHandler
     : IRequestHandler<DeleteOfficeLocationCommand>
    {
        private readonly IOfficeLocationRepository _repo;
        private readonly ILogger<DeleteOfficeLocationCommandHandler> _logger;

        public DeleteOfficeLocationCommandHandler(
            IOfficeLocationRepository repo,
            ILogger<DeleteOfficeLocationCommandHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task Handle(
            DeleteOfficeLocationCommand request,
            CancellationToken cancellationToken)
        {
            var existing = await _repo.GetByIdAsync(request.Id);
            if (existing is null)
            {
                _logger.LogWarning("OfficeLocation {Id} not found for deletion", request.Id);
                return;
            }

            await _repo.DeleteAsync(existing.Id);
            _logger.LogInformation("OfficeLocation {Id} deleted", request.Id);
        }
    }
}
