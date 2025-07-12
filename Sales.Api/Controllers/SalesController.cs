using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Api.Model.Models;
using Sales.Api.Services.Interfaces;
using Sales.Domain.Model.Models;

namespace Sales.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SalesController : ControllerBase
    {
        private readonly ISalesServices _salesServices;

        public SalesController(ISalesServices salesServices)
        {
            _salesServices = salesServices ?? throw new ArgumentNullException(nameof(salesServices));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesHeaderModel>>> GetAll()
        {
            var products = await _salesServices.ListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<SalesHeaderModel>> Register([FromBody] SalesCreatableDto creatableDto)
        {
            var model = await _salesServices.CreateSalesAsync(creatableDto);
            return Ok(model);
        }
    }
}
