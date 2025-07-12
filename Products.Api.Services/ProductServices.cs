using Core.Extensions;
using Core.Validation;
using FluentValidation;
using Products.Api.Model.Models;
using Products.Api.Services.Interfaces;
using Products.Api.Services.Models;
using Products.Domain.Model.Models;
using Products.Domain.Services.Interfaces;
using Products.Repositories.Interfaces;

namespace Products.Api.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepositories _productRepositories;
        private readonly IModelValidatorResolver _validatorResolver;
        private readonly IProductDomainServices _productDomainServices;

        public ProductServices(
            IProductRepositories productRepositories,
            IModelValidatorResolver validatorResolver,
            IProductDomainServices productDomainServices
            )
        {
            _productRepositories = productRepositories ?? throw new ArgumentNullException(nameof(productRepositories));
            _validatorResolver = validatorResolver ?? throw new ArgumentNullException(nameof(validatorResolver));
            _productDomainServices = productDomainServices ?? throw new ArgumentNullException(nameof(productDomainServices));
        }

        public async Task<IEnumerable<ProductDto>> ListAsync()
        {
            return await _productDomainServices.ListAsync();
        }

        public async Task<IEnumerable<KardexDto>> GetCardexAsync()
        {
            return await _productDomainServices.GetKardexProductAsync();
        }

        public async Task<ProductModel> RegisterAsync(ProductCreatableDto creatableDto)
        {
            creatableDto.ValidateArgumentOrThrow(nameof(creatableDto));

            var validator = _validatorResolver.GetValidator<ProductCreatableDto>();
            validator.ValidateAndThrow(creatableDto);

            ProductModel model = BuildProductModel(creatableDto);

            await _productRepositories.InsertAsync(model);

            return model;
        }

        public async Task<ProductModel> UpdateAsync(Guid id, ProductUpdatableDto updatableDto)
        {
            updatableDto.ValidateArgumentOrThrow(nameof(updatableDto));

            ProductModel model = await _productRepositories.GetByIdAsync(id);

            model.ValidateExistOrThrow(id);


            if (!string.IsNullOrEmpty(updatableDto.Name))
                model.Name = updatableDto.Name;

            if (updatableDto.LotNumber != null)
                model.LotNumber = updatableDto.LotNumber.Value;

            if (updatableDto.SalePrice.HasValue)
                model.SalePrice = updatableDto.SalePrice.Value;

            if (updatableDto.Cost.HasValue)
            {
                model.Cost = updatableDto.Cost.Value;
                model.SalePrice = updatableDto.Cost.Value * 1.35m;
            }

            _productRepositories.Update(model);

            return model;
        }

        private ProductModel BuildProductModel(ProductCreatableDto creatableDto)
        {
            return new ProductModel
            {
                Id = Guid.NewGuid(),
                Cost = creatableDto.Cost,
                LotNumber = creatableDto.LotNumber,
                Name = creatableDto.Name,
                SalePrice = creatableDto.SalePrice ??creatableDto.Cost * 1.35m,
                CreateDatetime = DateTime.UtcNow
            };
        }
    }
}
