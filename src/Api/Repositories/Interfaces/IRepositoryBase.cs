using System.Linq.Expressions;

namespace Api.Repositories.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Checks Database Health.  Returns true if database connection can be established
        /// </summary>
        /// <returns>boolean</returns>
        Task<bool> IsHealthy();

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetById(int id);

        /// <summary>
        /// Gets the by identifier detached.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        T GetByIdDetached(int id);

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> GetAllAsync();

        /// <summary>Gets the by identifier asynchronous.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ValueTask<T> GetByIdAsync(int id);

        /// <summary>Inserts the asynchronous.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> InsertAsync(T entity);

        /// <summary>Bulk inserts the list of entities.</summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<List<T>> BulkInsertAsync(List<T> entities);

        /// <summary>Bulk updates the list of entities.</summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<List<T>> BulkUpdateAsync(List<T> entities);

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity, int id);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Task<int> DeleteAsync(int id);

        /// <summary>
        /// Finds the asynchronous.
        /// </summary>
        /// <param name="match">The match.</param>
        /// <returns></returns>
        Task<T> FindAsync(Expression<Func<T, bool>> match);

        /// <summary>
        /// Finds the many asynchronous.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns></returns>
        Task<ICollection<T>> FindManyAsync(Expression<Func<T, bool>> where);

        /// <summary>
        /// Finds the many with a group by clause.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="groupBy">The group by.</param>
        /// <returns></returns>
        ICollection<int?> FindManyGroupByInt(Expression<Func<T, bool>> where, Func<T, int?> groupBy);
    }
}
