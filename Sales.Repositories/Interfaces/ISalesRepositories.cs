using Core.EntityFrameworkCore;
using Sales.Domain.Model.Models;

namespace Sales.Repositories.Interfaces
{
    public interface ISalesRepositories : IEntityFrameworkGenericRepository<SalesHeaderModel, Guid>
    {
        Task<IEnumerable<SalesHeaderModel>> GetAsync();
    }
}
