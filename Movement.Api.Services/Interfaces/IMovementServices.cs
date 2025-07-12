using Movement.Api.Model.Models;
using Movement.Domain.Model.Models;

namespace Movement.Api.Services.Interfaces
{
    public interface IMovementServices
    {
        Task<MovementHeaderModel> CreateMovementAsync(MovementCreatableDto dto);
        Task<IEnumerable<KardexProductDto>> GetSummaryMovementsAsync();
    }
}
