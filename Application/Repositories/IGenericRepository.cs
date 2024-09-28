using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets an entity
        /// </summary>
        /// <param name="filter">Lambda expression (returing boolean) to filter results. Example: user => user.LastName == "Smith"</param>
        /// <param name="orderBy">Lambda expression to order results. Example: q => q.OrderBy(u => u.LastName)</param>
        /// <returns>List of entities matching filters provided</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null);

        /// <summary>
        /// Gets an entity by Id
        /// </summary>
        /// <param name="id">Id of entity to recover</param>
        /// <returns>Resulting entity. NULL if it do not exist</returns>
        TEntity? GetById(long id);

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="entity">Entity to be created</param>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes entity by id
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        void Delete(object id);

        /// <summary>
        /// Deletes entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Saves changes made
        /// </summary>
        /// <returns>Number of entities affected</returns>
        int Save();
    }
}
