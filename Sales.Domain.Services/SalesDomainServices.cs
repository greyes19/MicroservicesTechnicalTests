using Core.Extensions;
using Sales.Api.Model.Models;
using Sales.Domain.Model.Models;
using Sales.Domain.Services.Interfaces;
using Sales.Repositories.Interfaces;

namespace Sales.Domain.Services
{
    public class SalesDomainServices : ISalesDomainServices
    {
        private readonly ISalesRepositories _salesRepositories;
        public SalesDomainServices(ISalesRepositories salesRepositories)
        {
            _salesRepositories = salesRepositories ?? throw new ArgumentNullException(nameof(salesRepositories));
        }

        public async Task<SalesHeaderModel> CreateSalesAsync(SalesCreatableDto dto)
        {
            dto.ValidateArgumentOrThrow(nameof(dto));

            DateTime dateTimeNow = DateTime.UtcNow;

            var details = dto.Details.Select(d => new SalesDetailModel
            {
                Id = Guid.NewGuid(),
                ProductId = d.ProductId,
                Quantity = d.Quantity,
                Price = d.Price,
                SubTotal = d.Price * d.Quantity,
                Igv = Math.Round(d.Price * d.Quantity * 0.18m, 2),
                Total = Math.Round(d.Price * d.Quantity * 1.18m, 2),
                CreateDatetime = dateTimeNow
            }).ToList();

            var subTotal = details.Sum(d => d.SubTotal);
            var igv = details.Sum(d => d.Igv);

            var purchaseHeader = new SalesHeaderModel
            {
                Id = Guid.NewGuid(),
                CreateDatetime = dateTimeNow,
                SubTotal = subTotal,
                Igv = igv,
                SalesDetails = details
            };

            SalesMovementCreatableDto movement = new SalesMovementCreatableDto
            {
                OriginDocumentId = purchaseHeader.Id,
                MovementType = (int)MovementType.MovementOut,
                Details = dto.Details.Select(d => new SalesMovementDetailsCreatableDto
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity
                }).ToList()
            };

            return await _salesRepositories.InsertAsync(purchaseHeader);
        }
    }
}
