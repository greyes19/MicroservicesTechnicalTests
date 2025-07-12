namespace Movement.Api.Model.Models
{
    public class KardexProductDto
    {
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public int MovementType { get; set; }
    }
}
