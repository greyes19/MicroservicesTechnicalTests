using Core.Extensions;
using Core.Validation;
using FluentValidation;
using Movement.Api.Model.Models;
using Movement.Api.Services.Interfaces;
using Movement.Domain.Model.Models;
using Movement.Domain.Services.Interfaces;
using Movement.Repositories.Interfaces;

namespace Movement.Api.Services
{
    public class MovementServices : IMovementServices
    {
        private readonly IMovementDomainService _movementDomainService;
        private readonly IModelValidatorResolver _validatorResolver;
        private readonly IMovementRepositories _movementRepositories;
        public MovementServices(
            IMovementDomainService movementDomainService,
            IModelValidatorResolver validatorResolver,
            IMovementRepositories movementRepositories
            )
        {
            _movementDomainService = movementDomainService ?? throw new ArgumentNullException(nameof(movementDomainService));
            _validatorResolver = validatorResolver ?? throw new ArgumentNullException(nameof(validatorResolver));
            _movementRepositories = movementRepositories ?? throw new ArgumentNullException(nameof(movementRepositories));
        }

        public async Task<MovementHeaderModel> CreateMovementAsync(MovementCreatableDto dto)
        {
            dto.ValidateArgumentOrThrow(nameof(dto));

            var validator = _validatorResolver.GetValidator<MovementCreatableDto>();
            validator.ValidateAndThrow(dto);

            return await _movementDomainService.CreateMovementAsync(dto);
        }

        public async Task<IEnumerable<KardexProductDto>> GetSummaryMovementsAsync()
        {
            var movements = await _movementRepositories.GetSummaryMovementsAsync();

            return movements;
        }

        public async Task<IEnumerable<MovementsDto>> GetMovementsAsync(Guid productId)
        {
            productId.ValidateArgumentOrThrow(nameof(productId));

            return await _movementRepositories.GetMovementsAsync(productId);
        }
    }
}
