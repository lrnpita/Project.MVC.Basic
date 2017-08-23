using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using DTO;

namespace DAL
{
    public interface IRepository<T> where T : BaseEntity
    {
        T FindByKey<TId>(TId id);
        void Delete(T entity);
        void Update(T entity);
        void Insert(T entity);
        void SaveChanges();
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindByInclude(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> All();
    }
}
