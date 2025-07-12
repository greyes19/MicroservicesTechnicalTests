
namespace Sales.Infraestructure.Management
{
    public interface IDomainApplicationUnitOfWork
    {
        Task BeginDomainTransactionAsync();
        Task CommitDomainTransactionAsync();
        Task RollbackDomainTransactionAsync();
    }
}
