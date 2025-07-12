using Core.EntityFrameworkCore;
using Products.Api.Model.Models;
using Products.Domain.Model.Models;

namespace Products.Repositories.Interfaces
{
    public interface IProductRepositories : IEntityFrameworkGenericRepository<ProductModel, Guid>
    {
        Task<IEnumerable<ProductDto>> GetAsync(List<MovementSummaryProductDto> movements);
        Task<List<KardexDto>> GetCardexAsync(List<MovementSummaryProductDto> movements);
    }
}
