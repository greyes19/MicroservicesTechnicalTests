namespace Sales.Domain.Model.Models
{
    public class SalesDetailModel
    {
        public Guid Id { get; set; }
        public Guid SalesHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateDatetime { get; set; }
        public virtual SalesHeaderModel SalesHeader { get; set; }
    }
}
