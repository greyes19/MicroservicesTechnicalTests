using System.Text.Json.Serialization;

namespace Sales.Domain.Model.Models
{
    public class SalesHeaderModel
    {
        public Guid Id { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Igv { get; set; }
        public DateTime CreateDatetime { get; set; }
        [JsonIgnore]
        public ICollection<SalesDetailModel> SalesDetails { get; set; }
        
    }
}
