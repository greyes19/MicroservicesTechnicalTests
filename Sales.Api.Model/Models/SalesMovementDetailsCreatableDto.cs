namespace Sales.Api.Model.Models
{
    public class SalesMovementDetailsCreatableDto
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}
