using Purchase.Api.Model.Models;

namespace Purchase.Domain.Services.Interfaces
{
    public interface IMovementHttpService
    {
        Task RegisterMovementAsync(PurchaseMovementCreatableDto dto);
    }
}
