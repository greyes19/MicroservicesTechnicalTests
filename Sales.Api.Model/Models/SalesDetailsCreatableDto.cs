namespace Sales.Api.Model.Models
{
    public class SalesDetailsCreatableDto
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
