
namespace Core.EntityFrameworkCore
{
    public interface IEntityFrameworkGenericRepository<TEntity, TIdentifier> where TEntity : class
    {
        void Delete(TEntity entityToDelete);
        Task<TEntity> GetByIdAsync(TIdentifier id);
        Task<TEntity> InsertAsync(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
