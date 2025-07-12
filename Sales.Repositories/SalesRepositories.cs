using Core.EntityFrameworkCore;
using Core.Validation;
using Microsoft.EntityFrameworkCore;
using Sales.Domain.Model.Models;
using Sales.Infraestructure;
using Sales.Repositories.Interfaces;

namespace Sales.Repositories
{
    public class SalesRepositories : EntityFrameworkGenericRepository<ApplicationDbContext, SalesHeaderModel, Guid>, ISalesRepositories
    {
        public SalesRepositories(ApplicationDbContext context, IModelValidatorResolver modelValidatorResolver) : base(context, modelValidatorResolver)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<SalesHeaderModel>> GetAsync()
        {
            return await _context.SalesHeaders
                .Include(h => h.SalesDetails)
                .ToListAsync();
        }
    }
}
