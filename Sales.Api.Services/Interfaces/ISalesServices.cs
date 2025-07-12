using Sales.Api.Model.Models;
using Sales.Domain.Model.Models;

namespace Sales.Api.Services.Interfaces
{
    public interface ISalesServices
    {
        Task<SalesHeaderModel> CreateSalesAsync(SalesCreatableDto dto);
        Task<IEnumerable<SalesHeaderModel>> ListAsync();
    }
}
