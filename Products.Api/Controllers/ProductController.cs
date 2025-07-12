using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.Api.Model.Models;
using Products.Api.Services.Interfaces;
using Products.Api.Services.Models;
using Products.Domain.Model.Models;

namespace Products.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices ?? throw new ArgumentNullException(nameof(productServices));
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productServices.ListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> Register([FromBody] ProductCreatableDto creatableDto)
        {
            var model = await _productServices.RegisterAsync(creatableDto);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductUpdatableDto productUpdatableDto)
        {
            var model = await _productServices.UpdateAsync(id, productUpdatableDto);

            return Ok(model);
        }

        [HttpGet("GetKardex")]
        public async Task<ActionResult<List<KardexDto>>> GetKardex()
        {
            var products = await _productServices.GetCardexAsync();
            return Ok(products);
        }
    }
}
