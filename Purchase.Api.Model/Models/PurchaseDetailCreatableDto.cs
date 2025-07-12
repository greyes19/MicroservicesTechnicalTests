namespace Purchase.Api.Model.Models;

public class PurchaseDetailCreatableDto
{
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
}
