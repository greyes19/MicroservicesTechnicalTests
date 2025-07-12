using Core.Extensions;
using Movement.Api.Model.Models;
using Movement.Domain.Model.Models;
using Movement.Domain.Services.Interfaces;
using Movement.Repositories.Interfaces;

namespace Movement.Domain.Services
{
    public class MovementDomainService : IMovementDomainService
    {
        private readonly IMovementRepositories _movementRepositories;
        public MovementDomainService(IMovementRepositories movementRepositories)
        {
            _movementRepositories = movementRepositories ?? throw new ArgumentNullException(nameof(movementRepositories));
        }

        public async Task<MovementHeaderModel> CreateMovementAsync(MovementCreatableDto dto)
        {
            dto.ValidateArgumentOrThrow(nameof(dto));

            if(dto.MovementType == MovementType.MovementOut)
            {

            }

            var details  = dto.Details.Select( d=> new MovementDetailModel
            {
                Id = Guid.NewGuid(),
                ProductId = d.ProductId,
                Quantity = d.Quantity

            }).ToList();

            var movementHeader = new MovementHeaderModel
            {
                Id = Guid.NewGuid(),
                MovementType = dto.MovementType,
                OriginDocumentId = dto.OriginDocumentId,
                CreateDatetime = DateTime.UtcNow,
                MovementDetails = details
            };

            return await _movementRepositories.InsertAsync(movementHeader);
        }
    }
}
