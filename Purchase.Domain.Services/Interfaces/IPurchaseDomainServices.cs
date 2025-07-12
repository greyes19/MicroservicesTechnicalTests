using Purchase.Api.Model.Models;
using Purchase.Domain.Model.Models;

namespace Purchase.Domain.Services.Interfaces
{
    public interface IPurchaseDomainServices
    {
        Task<PurchaseHeaderModel> CreatePurchaseAsync(PurchaseCreatableDto dto);
    }
}
