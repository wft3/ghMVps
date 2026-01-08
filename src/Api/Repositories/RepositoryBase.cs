using Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EFCore.BulkExtensions;

namespace Api.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class  //NOSONAR
    {
        #region Properties        

        /// <summary>
        /// The database context
        /// </summary>
        internal readonly DbContext _dbContext;
        /// <summary>
        /// The database set
        /// </summary>
        internal readonly DbSet<T> _dbSet;


        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="_context">The context.</param>
        protected RepositoryBase(DbContext _context)
        {
            _dbContext = _context;
            _dbSet = _dbContext.Set<T>();
        }

        #region Implementation
        public virtual async Task<bool> IsHealthy()
        {
            try
            {
                await _dbContext.Database.OpenConnectionAsync();
                await _dbContext.Database.CloseConnectionAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        /// <summary>
        /// Gets the by identifier detached.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetByIdDetached(int id)
        {
            var entity = _dbContext.Set<T>().Find(id);
            _dbContext.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual ValueTask<T> GetByIdAsync(int id)
        {
            return _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Inserts the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<T> InsertAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Bulk inserts asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<List<T>> BulkInsertAsync(List<T> entities)
        {
            await _dbContext.BulkInsertAsync(entities);
            return entities;
        }

        /// <summary>
        /// Bulk updates asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public virtual async Task<List<T>> BulkUpdateAsync(List<T> entities)
        {
            await _dbContext.BulkUpdateAsync(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<T> UpdateAsync(T entity, int id)
        {
            if (entity == null)
                return null;

            T existing = await _dbContext.Set<T>().FindAsync(id);
            if (existing != null)
            {
                _dbContext.Entry(existing).CurrentValues.SetValues(entity);
                await _dbContext.SaveChangesAsync();
            }
            return existing;
        }

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(int id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);

            if (entityToDelete == null) return 0;

            _dbSet.Remove(entityToDelete);
            return await _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Finds the specified match.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public virtual T Find(Expression<Func<T, bool>> match)
        {
            return _dbSet.SingleOrDefault(match);
        }

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _dbSet.SingleOrDefaultAsync(match);
        }

        /// <summary>
        /// Finds the many asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public virtual async Task<ICollection<T>> FindManyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        /// <summary>
        /// Finds the many asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        public virtual ICollection<int?> FindManyGroupByInt(Expression<Func<T, bool>> where, Func<T, int?> groupBy)
        {
            return _dbSet.Where(where).GroupBy(groupBy).Select(g => g.FirstOrDefault()).Select(groupBy).ToList();
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        #endregion
    }
}
