using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IService<T> where T : class
    {
       
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression); //Daha fazla performans sağlar, veri tabanına yapılacak olan sorguyu oluşturuyor.
        Task AddAsync(T entity); 
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity); // asenkron metodu yok çünkü uzun süren bir işlem değil.
        void RemoveRange(IEnumerable<T> entities);
        void  UpdateAsync(T entity);
    }
}
