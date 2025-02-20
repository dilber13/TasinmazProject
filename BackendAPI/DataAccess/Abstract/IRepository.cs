using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendAPI.DataAccess.Abstract
{
    /*
        IRepository interface'i, veritabanındaki nesnelerle ilgili CRUD işlemlerini içerir.
        Bu interface'i uygulayan Repository sınıfı, veritabanı işlemlerini gerçekleştirir.
    */

    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}