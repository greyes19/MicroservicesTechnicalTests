using Core.EntityFrameworkCore;
using Movement.Api.Model.Models;
using Movement.Domain.Model.Models;

namespace Movement.Repositories.Interfaces
{
    public interface IMovementRepositories : IEntityFrameworkGenericRepository<MovementHeaderModel, Guid>
    {
        Task<IEnumerable<MovementsDto>> GetMovementsAsync(Guid productId);
        Task<IEnumerable<KardexProductDto>> GetSummaryMovementsAsync();
    }
}
