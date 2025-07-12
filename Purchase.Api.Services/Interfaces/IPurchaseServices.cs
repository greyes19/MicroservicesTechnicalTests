using Purchase.Api.Model.Models;
using Purchase.Domain.Model.Models;

namespace Purchase.Api.Services.Interfaces
{
    public interface IPurchaseServices
    {
        Task<PurchaseHeaderModel> CreatePurchaseAsync(PurchaseCreatableDto dto);
        Task<IEnumerable<PurchaseHeaderModel>> ListAsync();
    }
}
