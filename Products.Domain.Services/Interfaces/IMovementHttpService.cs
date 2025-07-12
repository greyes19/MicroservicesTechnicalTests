using Products.Api.Model.Models;

namespace Products.Domain.Services.Interfaces
{
    public interface IMovementHttpService
    {
        Task<List<MovementSummaryProductDto>> GetSummaryAsync();
    }
}
