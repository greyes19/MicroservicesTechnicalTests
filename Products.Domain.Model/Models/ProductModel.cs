namespace Products.Domain.Model.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int LotNumber { get; set; }
        public DateTime CreateDatetime { get; set; }
        public decimal Cost { get; set; }
        public decimal SalePrice { get; set; }
    }
}
