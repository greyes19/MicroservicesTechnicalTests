
using Purchase.Domain.Model.Models;

namespace Purchase.Api.Model.Models
{
    public class PurchaseMovementCreatableDto
    {
        public int MovementType { get; set; }
        public Guid OriginDocumentId { get; set; }
        public List<PurchaseMovementDetailsCreatableDto> Details { get; set; }
    }
}
