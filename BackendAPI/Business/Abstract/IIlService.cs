using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Entities;
using System.Linq.Expressions;

namespace BackendAPI.Business.Abstract
{
    public interface IIlService
    {
        Task<IEnumerable<Il>> GetAllAsync(params Expression<Func<Il, object>>[] includes);
        Task<Il> GetByIdAsync(int id, params Expression<Func<Il, object>>[] includes);
        Task<Il> CreateIlAsync(Il il);
        Task UpdateeIlAsync(Il il);
        Task DeleteeIlAsync(int id);
    }

}