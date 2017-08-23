using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq.Expressions;
using DTO;

namespace DAL
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly DbContext dbContext;
        private DbSet<T> _entities;

        public Repository(DbContext context)
        {
            this.dbContext = context;
        }

        public T FindByKey<TId>(TId id)
        {
            var item = Expression.Parameter(typeof(T), "entity");
            var prop = Expression.Property(item, typeof(T).Name + "Id");
            var value = Expression.Constant(id);
            var equal = Expression.Equal(prop, value);
            var lambda = Expression.Lambda<Func<T, bool>>(equal, item);

            return Entities.AsNoTracking().SingleOrDefault(lambda);
        }

        public void Delete(T entity)
        {
            Entities.Attach(entity);
            Entities.Remove(entity);
        }

        public void Insert(T entity)
        {
            Entities.Add(entity);
        }

        public void Update(T entity)
        {
            Entities.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            //dbContext.FixState();
            dbContext.SaveChanges();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> results = Entities.AsNoTracking()
                .Where(predicate).ToList();

            return results;
        }

        public IEnumerable<T> All()
        {
            return Entities.AsNoTracking().ToList();
        }

        public IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            return GetAllIncluding(includeProperties).ToList();
        }

        public IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = GetAllIncluding(includeProperties);
            IEnumerable<T> results = query.Where(predicate).ToList();

            return results;
        }

        private IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = Entities.AsNoTracking();

            return includeProperties.Aggregate(queryable, (current, includeProperty) => current.Include(includeProperty));
        }

        /// <summary>
        /// Preload a list of entities
        /// </summary>
        private DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                {
                    _entities = dbContext.Set<T>();
                }

                return _entities;
            }
        }
    }
}
