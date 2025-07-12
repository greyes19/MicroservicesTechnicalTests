using Core.EntityFrameworkCore;
using Purchase.Domain.Model.Models;

namespace Purchase.Repositories.Interfaces
{
    public interface IPurchaseRepositories : IEntityFrameworkGenericRepository<PurchaseHeaderModel, Guid>
    {
        Task<IEnumerable<PurchaseHeaderModel>> GetAsync();
    }
}
