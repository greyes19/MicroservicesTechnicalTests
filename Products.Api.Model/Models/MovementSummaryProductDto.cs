using Products.Domain.Model.Models;

namespace Products.Api.Model.Models
{
    public class MovementSummaryProductDto
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public MovementType MovementType { get; set; }
    }
}
