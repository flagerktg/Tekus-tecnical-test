using Application.Repositories;
using Infrastructure.SQLServer;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class GenericSqlRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected Lazy<SqlServerDbContext> Context { get; }

        public GenericSqlRepository(
            Lazy<SqlServerDbContext> context
        )
        {
            Context = context;
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null)
        {
            IQueryable<TEntity> query = GetBaseEntityQuery();

            if (filter != null)
                query = query.Where(filter);

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public virtual TEntity? GetById(long id) =>
            GetBaseEntityQuery()?.Where(e => e.Id == id).FirstOrDefault();

        public virtual IEnumerable<TEntity> ListAll() =>
            GetBaseEntityQuery().ToList();

        public virtual TEntity Create(TEntity entity)
        {
            var result = Context.Value.Set<TEntity>().Add(entity).Entity;
            return result;
        }

        public virtual void Delete(object id)
        {
            TEntity? entityToDelete = Context.Value.Set<TEntity>().Find(id);
            if (entityToDelete != null)
                Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entity)
        {
            if (Context.Value.Entry(entity).State == EntityState.Detached)
                Context.Value.Set<TEntity>().Attach(entity);

            Context.Value.Set<TEntity>().Remove(entity);
        }

        public virtual TEntity Update(TEntity entity)
        {
            EntityEntry<TEntity> entry = Context.Value.Set<TEntity>().Attach(entity);
            Context.Value.Entry(entity).State = EntityState.Modified;
            return entry.Entity;
        }


        public int Save()
        {
            return Context.Value.SaveChanges();
        }

        /// <summary>
        /// Gets the base entity query which all basic GET operations are going to be based
        /// </summary>
        /// <returns>Base entity query</returns>
        protected virtual IQueryable<TEntity> GetBaseEntityQuery() =>
            Context.Value.Set<TEntity>();
    }
}
