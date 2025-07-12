using Core.Extensions;
using Core.Validation;
using FluentValidation;
using Purchase.Api.Model.Models;
using Purchase.Api.Services.Interfaces;
using Purchase.Domain.Model.Models;
using Purchase.Domain.Services.Interfaces;
using Purchase.Infraestructure.Management;
using Purchase.Repositories.Interfaces;

namespace Purchase.Api.Services
{
    public class PurchaseServices : IPurchaseServices
    {
        private readonly IPurchaseDomainServices _purchaseDomainServices;
        private readonly IPurchaseRepositories _purchaseRepositories;
        private readonly IModelValidatorResolver _validatorResolver;
        private readonly IDomainApplicationUnitOfWork _unitOfWork;
        public PurchaseServices(
            IPurchaseDomainServices purchaseDomainServices,
            IPurchaseRepositories purchaseRepositories,
            IModelValidatorResolver modelValidatorResolver,
            IDomainApplicationUnitOfWork unitOfWork
            )
        {
            _purchaseDomainServices = purchaseDomainServices ?? throw new ArgumentNullException(nameof(purchaseDomainServices));
            _purchaseRepositories = purchaseRepositories ?? throw new ArgumentNullException(nameof(purchaseRepositories));
            _validatorResolver = modelValidatorResolver ?? throw new ArgumentNullException(nameof(modelValidatorResolver));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<PurchaseHeaderModel>> ListAsync()
        {
            return await _purchaseRepositories.GetAsync();
        }

        public async Task<PurchaseHeaderModel> CreatePurchaseAsync(PurchaseCreatableDto dto)
        {
            dto.ValidateArgumentOrThrow(nameof(dto));

            var validator = _validatorResolver.GetValidator<PurchaseCreatableDto>();
            validator.ValidateAndThrow(dto);

            await _unitOfWork.BeginDomainTransactionAsync();

            try
            {
                var model = await _purchaseDomainServices.CreatePurchaseAsync(dto);

                await _unitOfWork.CommitDomainTransactionAsync();

                return model;
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackDomainTransactionAsync();

                throw;
            }
            
        }
    }
}
