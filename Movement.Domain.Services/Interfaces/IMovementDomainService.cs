using Movement.Api.Model.Models;
using Movement.Domain.Model.Models;

namespace Movement.Domain.Services.Interfaces
{
    public interface IMovementDomainService
    {
        Task<MovementHeaderModel> CreateMovementAsync(MovementCreatableDto dto);
    }
}
