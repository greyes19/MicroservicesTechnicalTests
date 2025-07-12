using Products.Api.Model.Models;

namespace Products.Domain.Services.Interfaces
{
    public interface IProductDomainServices
    {
        Task<List<KardexDto>> GetKardexProductAsync();
    }
}
