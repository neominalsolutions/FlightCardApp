using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FlightCardApp.libs.core
{

    public interface IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        TEntity Find(string key);
        IQueryable<TEntity> SelectWhere(Expression<Func<TEntity, bool>> lamda);
        IQueryable<TEntity> Select();
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(string key);
        bool Save(); // UnitOfWork yötemini kullanmak için Save diğer operasyonların içinden ayrılmıştır. Transaction Scope yönetebilir.

    }

    public abstract class EFBaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity,IAggregateRoot   
        {

        protected DbContext _dbContext;
        protected DbSet<TEntity> _dbSet;

        public EFBaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }


        public virtual void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public virtual TEntity Find(string Id)
        {
            return _dbSet.Find(Id);
        }

        public virtual IQueryable<TEntity> Select()
        {
            return _dbSet.AsQueryable();
        }

        public virtual IQueryable<TEntity> SelectWhere(Expression<Func<TEntity,bool>> lambda)
        {
            return _dbSet.Where(lambda).AsQueryable();
        }

        public virtual void Remove(string Id)
        {
            var entity = Find(Id);
            _dbSet.Remove(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual bool  Save()
        {
            return _dbContext.SaveChanges() > 0 ? true:false;
        }

        
    }
}
