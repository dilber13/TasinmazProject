using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Entities;
using System.Linq.Expressions;

namespace BackendAPI.Business.Abstract
{
    public interface IMahalleService
    {
        Task<IEnumerable<Mahalle>> GetAllAsync(params Expression<Func<Mahalle, object>>[] includes);
        Task<Mahalle> GetByIdAsync(int id, params Expression<Func<Mahalle, object>>[] includes);
        Task<Mahalle> CreateMahalleAsync(Mahalle mahalle);
        Task UpdateMahalleAsync(Mahalle mahalle);
        Task DeleteMahalleAsync(int id);
    }
}