using Products.Api.Services.Models;
using Products.Domain.Services.Models;

namespace Products.Api.Services.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductModel>> ListAsync();
        Task<ProductModel> RegisterAsync(ProductCreatableDto creatableDto);
        Task<ProductModel> UpdateAsync(Guid id, ProductUpdatableDto updatableDto);
    }
}
