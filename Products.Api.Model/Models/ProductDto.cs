namespace Products.Api.Model.Models
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int LotNumber { get; set; }
        public DateTime CreateDatetime { get; set; }
        public decimal Cost { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Stock { get; set; }
    }
}
