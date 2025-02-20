using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendAPI.Business.Concrete
{
    public class IlceService : IIlceService
    {
        private readonly IRepository<Ilce> _repository;

        public IlceService(IRepository<Ilce> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Ilce>> GetAllAsync(params Expression<Func<Ilce, object>>[] includes)
        {
            return await _repository.GetAllAsync(includes);
        }

        public async Task<Ilce> GetByIdAsync(int id, params Expression<Func<Ilce, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        public async Task<Ilce> CreateIlceAsync(Ilce ilce)
        {
            return await _repository.AddAsync(ilce);
        }

        public async Task UpdateIlceAsync(Ilce ilce)
        {
            await _repository.UpdateAsync(ilce);
        }

        public async Task DeleteIlceAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}