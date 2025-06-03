using System.Linq.Expressions;
using LibraryDomain;
using Microsoft.EntityFrameworkCore.Query;

namespace LibraryRepository.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);

        // books.Where(book => book.Equals(id)).Select(book => book.Id)
        E? Get<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

        IEnumerable<E> GetAll<E>(Expression<Func<T, E>> selector,
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    }
}
