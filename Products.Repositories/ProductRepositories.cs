using Core.EntityFrameworkCore;
using Core.Validation;
using Microsoft.EntityFrameworkCore;
using Products.Api.Model.Models;
using Products.Domain.Model.Models;
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

        public async Task<IEnumerable<ProductDto>> GetAsync(List<MovementSummaryProductDto> movements)
        {
            var productIds = movements.Select(m => m.ProductId).Distinct().ToList();//Se traen los movimientos del microservicios Movements
            var products = await _context.Products.ToListAsync();

            var query = from  pr in products
                        join m in movements on pr.Id equals m.ProductId
                        group new { m, pr } by m.ProductId into g
                        select new ProductDto
                        {
                            Id = g.Key,
                            Name = g.First().pr.Name,
                            Cost = g.First().pr.Cost,
                            SalePrice = g.First().pr.SalePrice,
                            CreateDatetime = g.First().pr.CreateDatetime,
                            LotNumber = g.First().pr.LotNumber,
                            Stock = g.Where(x => x.m.MovementType == MovementType.MovementIn).Sum(x => x.m.Quantity)
                        - g.Where(x => x.m.MovementType == MovementType.MovementOut).Sum(x => x.m.Quantity)
                        };
            return query.ToList();
        }

        public async Task<List<KardexDto>> GetCardexAsync(List<MovementSummaryProductDto> movements)
        {
            var productIds = movements.Select(m => m.ProductId).Distinct().ToList();//Se traen los movimientos del microservicios Movements
            var products = await _context.Products.ToListAsync();

            var query = from pr in products
                        join m in movements on pr.Id equals m.ProductId
                        group new { m, pr } by m.ProductId into g
                        select new KardexDto
                        {
                            ProductId = g.Key,
                            Name = g.First().pr.Name,
                            Cost = g.First().pr.Cost,
                            SalePrice = g.First().pr.SalePrice,
                            Stock = g.Where(x => x.m.MovementType == MovementType.MovementIn).Sum(x => x.m.Quantity)
                        - g.Where(x => x.m.MovementType == MovementType.MovementOut).Sum(x => x.m.Quantity)
                        };

            return  query.ToList();
        }

    }
}
