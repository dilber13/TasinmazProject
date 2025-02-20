using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Entities;
using System.Linq.Expressions;

namespace BackendAPI.Business.Abstract
{
    public interface IIlceService
    {

        Task<IEnumerable<Ilce>> GetAllAsync(params Expression<Func<Ilce, object>>[] includes);
        Task<Ilce> GetByIdAsync(int id, params Expression<Func<Ilce, object>>[] includes);
        Task<Ilce> CreateIlceAsync(Ilce ilce);
        Task UpdateIlceAsync(Ilce ilce);
        Task DeleteIlceAsync(int id);
    }
}