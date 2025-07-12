using Core.EntityFrameworkCore;
using Sales.Api.Model.Models;
using Sales.Domain.Model.Models;

namespace Sales.Domain.Services.Interfaces
{
    public interface ISalesDomainServices
    {
        Task<SalesHeaderModel> CreateSalesAsync(SalesCreatableDto dto);
    }
}
