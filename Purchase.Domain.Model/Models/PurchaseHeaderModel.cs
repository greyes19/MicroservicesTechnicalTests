using System.Text.Json.Serialization;

namespace Purchase.Domain.Model.Models
{
    public class PurchaseHeaderModel
    {
        public Guid Id { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateDatetime { get; set; }
        [JsonIgnore]
        public ICollection<PurchaseDetailModel> PurchaseDetails { get; set; }
    }
}
