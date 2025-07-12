using Core.EntityFrameworkCore;
using Core.Validation;
using Microsoft.EntityFrameworkCore;
using Purchase.Domain.Model.Models;
using Purchase.Infraestructure;
using Purchase.Repositories.Interfaces;

namespace Purchase.Repositories
{
    public class PurchaseRepositories : EntityFrameworkGenericRepository<ApplicationDbContext, PurchaseHeaderModel, Guid>, IPurchaseRepositories
    {
        public PurchaseRepositories(ApplicationDbContext context, IModelValidatorResolver modelValidatorResolver) : base(context, modelValidatorResolver)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<PurchaseHeaderModel>> GetAsync()
        {
            return await _context.PurchaseHeaders
                .Include(h => h.PurchaseDetails)
                .ToListAsync();
        }
    }
}
