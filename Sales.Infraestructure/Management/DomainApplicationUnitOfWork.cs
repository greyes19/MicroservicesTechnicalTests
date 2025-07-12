using Microsoft.EntityFrameworkCore.Storage;

namespace Sales.Infraestructure.Management
{
    public class DomainApplicationUnitOfWork : IDomainApplicationUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private IDbContextTransaction _transaction;

        public DomainApplicationUnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task BeginDomainTransactionAsync()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitDomainTransactionAsync()
        {
            await _dbContext.SaveChangesAsync();
            await _transaction?.CommitAsync();
        }

        public async Task RollbackDomainTransactionAsync()
        {
            await _transaction?.RollbackAsync();
        }
    }
}
