using Core.Extensions;
using Core.Validation;
using FluentValidation;
using Sales.Api.Model.Models;
using Sales.Api.Services.Interfaces;
using Sales.Domain.Model.Models;
using Sales.Domain.Services.Interfaces;
using Sales.Infraestructure.Management;
using Sales.Repositories.Interfaces;

namespace Sales.Api.Services
{
    public class SalesServices : ISalesServices
    {
        private readonly ISalesDomainServices _salesDomainServices;
        private readonly ISalesRepositories _salesRepositories;
        private readonly IModelValidatorResolver _validatorResolver;
        private readonly IDomainApplicationUnitOfWork _unitOfWork;

        public SalesServices(
            ISalesDomainServices salesDomainServices, 
            ISalesRepositories salesRepositories, 
            IModelValidatorResolver validatorResolver,
            IDomainApplicationUnitOfWork unitOfWork
            )
        {
            _salesDomainServices = salesDomainServices ?? throw new ArgumentNullException(nameof(salesDomainServices));
            _salesRepositories = salesRepositories ?? throw new ArgumentNullException(nameof(salesRepositories));
            _validatorResolver = validatorResolver ?? throw new ArgumentNullException(nameof(validatorResolver));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IEnumerable<SalesHeaderModel>> ListAsync()
        {
            return await _salesRepositories.GetAsync();
        }

        public async Task<SalesHeaderModel> CreateSalesAsync(SalesCreatableDto dto)
        {
            dto.ValidateArgumentOrThrow(nameof(dto));
            var validator = _validatorResolver.GetValidator<SalesCreatableDto>();
            validator.ValidateAndThrow(dto);


            await _unitOfWork.BeginDomainTransactionAsync();
            try
            {
                var model = await _salesDomainServices.CreateSalesAsync(dto);

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
