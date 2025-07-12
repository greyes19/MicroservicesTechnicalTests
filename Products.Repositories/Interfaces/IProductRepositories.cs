using Core.EntityFrameworkCore;
using Products.Api.Model.Models;
using Products.Domain.Model.Models;

namespace Products.Repositories.Interfaces
{
    public interface IProductRepositories : IEntityFrameworkGenericRepository<ProductModel, Guid>
    {
        Task<IEnumerable<ProductModel>> GetAsync();
        Task<List<KardexDto>> GetCardexAsync(List<MovementSummaryProductDto> movements);
    }
}
