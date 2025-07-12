using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movement.Api.Model.Models;
using Movement.Api.Services.Interfaces;
using Movement.Domain.Model.Models;

namespace Movement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MovementController : ControllerBase
    {
        private readonly IMovementServices _movementServices;

        public MovementController(IMovementServices movementServices)
        {
            _movementServices = movementServices ?? throw new ArgumentNullException(nameof(movementServices));
        }

        /// <summary>
        /// The create header and details of a movement.
        /// </summary>
        /// <param name="creatableDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<MovementHeaderModel>> Register([FromBody] MovementCreatableDto creatableDto)
        {
            var model = await _movementServices.CreateMovementAsync(creatableDto);
            return Ok(model);
        }

        /// <summary>
        /// Get all movements summary.
        /// </summary>
        /// <returns></returns>
        [HttpGet("summary")]
        public async Task<ActionResult<IEnumerable<KardexProductDto>>> GetSummary()
        {
            var models = await _movementServices.GetSummaryMovementsAsync();

            return Ok(models);
        }

        /// <summary>
        /// Get movement by product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("kardex/{productId}/movements")]
        public async Task<ActionResult<IEnumerable<MovementsDto>>> GetMovements(Guid productId)
        {
            var models = await _movementServices.GetMovementsAsync(productId);

            return Ok(models);
        }
    }
}
