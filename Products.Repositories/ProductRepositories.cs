using Core.EntityFrameworkCore;
using Core.Validation;
using Microsoft.EntityFrameworkCore;
using Products.Domain.Services.Models;
using Products.Infraestructure;
using Products.Repositories.Interfaces;

namespace Products.Repositories
{
    public class ProductRepositories : EntityFrameworkGenericRepository<ApplicationDbContext, ProductModel, Guid>, IProductRepositories
    {
        public ProductRepositories(ApplicationDbContext context, IModelValidatorResolver modelValidatorResolver) : base(context, modelValidatorResolver)
        {
            _context = context ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<ProductModel>> GetAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
