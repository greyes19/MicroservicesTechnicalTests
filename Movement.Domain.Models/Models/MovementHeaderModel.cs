using System.Text.Json.Serialization;

namespace Movement.Domain.Model.Models
{
    public class MovementHeaderModel
    {
        public Guid Id { get; set; }
        public MovementType MovementType { get; set; }
        public Guid OriginDocumentId { get; set; }
        public DateTime CreateDatetime { get; set; }
        [JsonIgnore]
        public ICollection<MovementDetailModel> MovementDetails { get; set; }
    }
}
