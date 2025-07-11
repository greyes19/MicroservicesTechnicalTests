using Core.Extensions;
using Core.Validation;
using FluentValidation;
using Products.Api.Services.Interfaces;
using Products.Api.Services.Models;
using Products.Domain.Services.Models;
using Products.Repositories.Interfaces;
using System.Collections;

namespace Products.Api.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepositories _productRepositories;
        private readonly IModelValidatorResolver _validatorResolver;

        public ProductServices(
            IProductRepositories productRepositories,
            IModelValidatorResolver validatorResolver
            )
        {
            _productRepositories = productRepositories ?? throw new ArgumentNullException(nameof(productRepositories));
            _validatorResolver = validatorResolver ?? throw new ArgumentNullException(nameof(validatorResolver));
        }

        public async Task<IEnumerable<ProductModel>> ListAsync()
        {
            return await _productRepositories.GetAsync();
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

            model.Name = updatableDto.Name;
            model.LotNumber = updatableDto.LotNumber;
            model.SalePrice = updatableDto.SalePrice;    
            model.Cost = updatableDto.Cost;

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
                SalePrice = creatableDto.SalePrice,
                CreateDatetime = DateTime.UtcNow
            };
        }
    }
}
