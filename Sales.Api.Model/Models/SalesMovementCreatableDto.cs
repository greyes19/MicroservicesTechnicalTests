using Sales.Domain.Model.Models;

namespace Sales.Api.Model.Models
{
    public class SalesMovementCreatableDto
    {
        public int MovementType { get; set; }
        public Guid OriginDocumentId { get; set; }
        public List<SalesMovementDetailsCreatableDto> Details { get; set; }
    }
}
