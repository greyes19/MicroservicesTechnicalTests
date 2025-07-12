namespace Movement.Domain.Model.Models
{
    public class MovementDetailModel
    {
        public Guid Id { get; set; }
        public Guid MovementHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public virtual MovementHeaderModel MovementHeader { get; set; }
    }
}
