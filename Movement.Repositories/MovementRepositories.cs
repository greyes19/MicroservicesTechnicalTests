using Core.EntityFrameworkCore;
using Core.Extensions;
using Core.Validation;
using Microsoft.EntityFrameworkCore;
using Movement.Api.Model.Models;
using Movement.Domain.Model.Models;
using Movement.Infraestructure;
using Movement.Repositories.Interfaces;

namespace Movement.Repositories
{
    public class MovementRepositories : EntityFrameworkGenericRepository<ApplicationDbContext, MovementHeaderModel, Guid>, IMovementRepositories
    {
        public MovementRepositories(ApplicationDbContext context, IModelValidatorResolver modelValidatorResolver) : base(context, modelValidatorResolver)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<MovementHeaderModel>> GetAsync()
        {
            return await _context.MovementHeaders.Include(x => x.MovementDetails).ToListAsync();
        }

        public async Task<IEnumerable<KardexProductDto>> GetSummaryMovementsAsync()
        {
            
            var query = from m in _context.MovementHeaders
                        join d in _context.MovementDetails on m.Id equals d.MovementHeaderId
                        group d by new { d.ProductId, m.MovementType } into g
                        select new KardexProductDto
                        {
                            ProductId = g.Key.ProductId,
                            MovementType = (int)g.Key.MovementType,
                            Quantity = g.Sum(x => x.Quantity)
                        };            

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<MovementsDto>> GetMovementsAsync(Guid productId)
        {
            productId.ValidateArgumentOrThrow(nameof(productId));

            var query = from m in _context.MovementHeaders
                        join d in _context.MovementDetails on m.Id equals d.MovementHeaderId
                        where d.ProductId == productId
                        select new MovementsDto
                        {
                            Id = m.Id,
                            Date = m.CreateDatetime,
                            MovementType = MovementType.MovementIn == m.MovementType ? "Ingreso" : "Salida",
                            Quantity = d.Quantity,
                        };

           return await query.OrderBy(x=>x.Id).ToListAsync();
        }
    }
}
