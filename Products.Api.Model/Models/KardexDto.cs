namespace Products.Api.Model.Models
{
    public class KardexDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public decimal Stock { get; set; }
        public decimal Cost { get; set; }
        public decimal SalePrice { get; set; }
    }
}
