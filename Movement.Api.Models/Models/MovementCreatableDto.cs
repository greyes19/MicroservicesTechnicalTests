using Movement.Domain.Model.Models;

namespace Movement.Api.Model.Models
{
    public class MovementCreatableDto
    {
        public MovementType MovementType { get; set; }
        public Guid OriginDocumentId { get; set; }
        public List<MovementDetailCreatableDto> Details { get; set; }
    }

}
