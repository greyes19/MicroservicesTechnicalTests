namespace Purchase.Api.Model.Models
{
    public class PurchaseProductUpdatableDto
    {
        public string Name { get; set; }
        public int? LotNumber { get; set; }
        public decimal? Cost { get; set; }
        public decimal? SalePrice { get; set; }
    }
}
