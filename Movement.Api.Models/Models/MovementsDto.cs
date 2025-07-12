namespace Movement.Api.Model.Models
{
    public class MovementsDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string MovementType { get; set; }
        public decimal Quantity { get; set; }
    }
}
