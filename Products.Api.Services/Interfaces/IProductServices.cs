using Products.Api.Model.Models;
using Products.Api.Services.Models;
using Products.Domain.Model.Models;

namespace Products.Api.Services.Interfaces
{
    public interface IProductServices
    {
        Task<IEnumerable<KardexDto>> GetCardexAsync();
        Task<IEnumerable<ProductModel>> ListAsync();
        Task<ProductModel> RegisterAsync(ProductCreatableDto creatableDto);
        Task<ProductModel> UpdateAsync(Guid id, ProductUpdatableDto updatableDto);
    }
}
