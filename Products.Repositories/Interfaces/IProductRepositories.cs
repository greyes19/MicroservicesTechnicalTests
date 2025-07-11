using Core.EntityFrameworkCore;
using Products.Domain.Services.Models;

namespace Products.Repositories.Interfaces
{
    public interface IProductRepositories : IEntityFrameworkGenericRepository<ProductModel, Guid>
    {
        Task<IEnumerable<ProductModel>> GetAsync();
    }
}
