using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoolCopter.SharedKernel.Repository
{
    public interface IEntityRepository<TEntity> : IDisposable
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> All { get; }

        IQueryable<TEntity> AllWithNoTracking { get; }

        IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);

        int Create(TEntity entity);

        int Delete(TEntity entity);

        int Update(TEntity entity);
    }

    public class EntityRepository<TEntity> : IEntityRepository<TEntity>
        where TEntity : BaseEntity
    {
        #region Local Members

        protected readonly DbContext Context;

        #endregion Local Members

        public EntityRepository(DbContext context)
        {
            if (context != null)
                Context = context;
            else
                throw new ArgumentNullException("context");
        }

        public IQueryable<TEntity> All => Context.Set<TEntity>().AsQueryable();

        public IQueryable<TEntity> AllWithNoTracking => Context.Set<TEntity>().AsNoTracking();

        public void Attach(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
        }

        public void AttachRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                Context.Set<TEntity>().Attach(entity);
        }

        public IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = All;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public int Create(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Context.Set<TEntity>().Add(entity);
            var result = Context.SaveChanges(true);
            return result;
        }

        public int Delete(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Context.Set<TEntity>().Attach(entity);
            Context.Set<TEntity>().Remove(entity);
            var result = Context.SaveChanges(true);
            return result;
        }

        public int Update(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            Context.Entry(entity).State = EntityState.Modified;
            var result = Context.SaveChanges(true);
            return result;
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}