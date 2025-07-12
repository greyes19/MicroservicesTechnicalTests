using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Purchase.Api.Model.Models;
using Purchase.Api.Services.Interfaces;
using Purchase.Domain.Model.Models;

namespace Purchase.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseServices _purchaseServices;

        public PurchaseController(IPurchaseServices purchaseServices)
        {
            _purchaseServices = purchaseServices ?? throw new ArgumentNullException(nameof(purchaseServices));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseHeaderModel>>> GetAll()
        {
            var products = await _purchaseServices.ListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseHeaderModel>> Register([FromBody] PurchaseCreatableDto creatableDto)
        {
            var model = await _purchaseServices.CreatePurchaseAsync(creatableDto);
            return Ok(model);
        }

    }
}
