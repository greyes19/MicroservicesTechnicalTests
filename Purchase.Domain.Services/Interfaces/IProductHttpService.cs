using Purchase.Api.Model.Models;

namespace Purchase.Domain.Services.Interfaces
{
    public interface IProductHttpService
    {
        Task UpdateProductAsync(Guid id, PurchaseProductUpdatableDto dto);
    }
}
