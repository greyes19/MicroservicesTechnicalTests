using Products.Api.Model.Models;
using Products.Domain.Services.Interfaces;
using Products.Repositories.Interfaces;

namespace Products.Domain.Services
{
    public class ProductDomainServices : IProductDomainServices
    {
        private readonly IProductRepositories _productRepositories;
        private readonly IMovementHttpService _movementHttpService;

        public ProductDomainServices(IProductRepositories productRepositories, IMovementHttpService movementHttpService)
        {
            _productRepositories = productRepositories ?? throw new ArgumentNullException(nameof(productRepositories));
            _movementHttpService = movementHttpService ?? throw new ArgumentNullException(nameof(movementHttpService));
        }

        public async Task<List<KardexDto>> GetKardexProductAsync()
        {
            List<MovementSummaryProductDto> movements =  await _movementHttpService.GetSummaryAsync();

            return await _productRepositories.GetCardexAsync(movements);
        }

    }
}
