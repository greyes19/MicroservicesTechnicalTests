using Core.Extensions;
using Purchase.Api.Model.Models;
using Purchase.Domain.Model.Models;
using Purchase.Domain.Services.Interfaces;
using Purchase.Repositories.Interfaces;

namespace Purchase.Domain.Services
{
    public class PurchaseDomainServices : IPurchaseDomainServices
    {
        private readonly IPurchaseRepositories _purchaseRepositories;
        private readonly IProductHttpService _productHttpService;
        private readonly IMovementHttpService _movementHttpService;
        public PurchaseDomainServices(
            IPurchaseRepositories purchaseRepositories,
            IProductHttpService productHttpService,
            IMovementHttpService movementHttpService
            )
        {
            _purchaseRepositories = purchaseRepositories ?? throw new ArgumentNullException(nameof(purchaseRepositories));
            _productHttpService = productHttpService ?? throw new ArgumentNullException(nameof(productHttpService));
            _movementHttpService = movementHttpService ?? throw new ArgumentNullException(nameof(movementHttpService));
        }

        public async Task<PurchaseHeaderModel> CreatePurchaseAsync(PurchaseCreatableDto dto)
        {
            dto.ValidateArgumentOrThrow(nameof(dto));

            foreach (var purchase in dto.PurchaseDetails)
            {
                purchase.ValidateArgumentOrThrow(nameof(purchase));

                await _productHttpService.UpdateProductAsync(purchase.ProductId, new PurchaseProductUpdatableDto
                {
                    Cost = purchase.Price,
                });
            }
            DateTime dateTimeNow = DateTime.UtcNow;

            var details = dto.PurchaseDetails.Select(d => new PurchaseDetailModel
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
            var total = details.Sum(d => d.Total);

            var purchaseHeader = new PurchaseHeaderModel
            {
                Id = Guid.NewGuid(),
                CreateDatetime = dateTimeNow,
                SubTotal = subTotal,
                Igv = igv,
                Total = total,
                PurchaseDetails = details
            };

            var model = await _purchaseRepositories.InsertAsync(purchaseHeader);
            //HACER MOVIMIENTO

            PurchaseMovementCreatableDto movement = new PurchaseMovementCreatableDto
            {
                OriginDocumentId = purchaseHeader.Id,
                MovementType = (int)MovementType.MovementIn,
                Details = dto.PurchaseDetails.Select(d => new PurchaseMovementDetailsCreatableDto
                {
                    ProductId = d.ProductId,
                    Quantity = d.Quantity
                }).ToList()
            };

            await _movementHttpService.RegisterMovementAsync(movement);

            return model;
        }
    }
}
