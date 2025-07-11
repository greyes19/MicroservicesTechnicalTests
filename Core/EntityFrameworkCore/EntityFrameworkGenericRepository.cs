using Core.Validation;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Core.EntityFrameworkCore
{
    public abstract class EntityFrameworkGenericRepository<TDbContext, TEntity, TIdentifier> : IEntityFrameworkGenericRepository<TEntity, TIdentifier>
        where TDbContext : DbContext
        where TEntity : class
    {
        public string DatabaseName { get; private set; }

        protected DbSet<TEntity> _dbSet;
        protected readonly IValidator<TEntity> _modelValidator;

        protected TDbContext _context;
        protected EntityFrameworkGenericRepository(TDbContext context, IModelValidatorResolver modelValidatorResolver)
        {
            _context = context;

            modelValidatorResolver = modelValidatorResolver ?? throw new ArgumentNullException(nameof(modelValidatorResolver));
            _dbSet = _context.Set<TEntity>();
            //if (_context.Database?.IsRelational() == true)
            //{
            //    DatabaseName = _context.Database.GetDbConnection().Database;
            //}
            _modelValidator = modelValidatorResolver.GetValidator<TEntity>();
        }
        public async Task<TEntity> GetByIdAsync(TIdentifier id)
        {
            ValidateIdentifier(id);

            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _modelValidator?.ValidateAndThrow(entity);

            EntityEntry<TEntity> result = await _dbSet.AddAsync(entity);

            await _context.SaveChangesAsync();

            return result.Entity;
        }

        public void Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null) throw new ArgumentNullException(nameof(entityToUpdate));

            _modelValidator?.ValidateAndThrow(entityToUpdate);

            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;

            _context.SaveChangesAsync();
        }

        public void Delete(TEntity entityToDelete)
        {
            if (entityToDelete == null) throw new ArgumentNullException(nameof(entityToDelete));

            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }
            _dbSet.Remove(entityToDelete);
        }

        public void ValidateIdentifier(object id)
        {
            switch (id)
            {
                case string identifier:
                    if (string.IsNullOrWhiteSpace(identifier)) throw new ArgumentNullException(nameof(id));
                    break;
                case Guid identifier:
                    if (identifier == Guid.Empty) throw new ArgumentException($"{nameof(id)} cannot be a Guid.Empty value", nameof(id));
                    break;
                default:
                    if (id == null) throw new ArgumentNullException(nameof(id));
                    break;
            }
        }
    }
}
