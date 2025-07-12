using Sales.Api.Model.Models;

namespace Sales.Domain.Services.Interfaces
{
    public interface IMovementHttpService
    {
        Task RegisterMovementAsync(SalesMovementCreatableDto dto);
    }
}
