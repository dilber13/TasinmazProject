using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendAPI.Business.Concrete
{
    public class MahalleService : IMahalleService
    {
        private readonly IRepository<Mahalle> _repository;

        public MahalleService(IRepository<Mahalle> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Mahalle>> GetAllAsync(params Expression<Func<Mahalle, object>>[] includes)
        {
            return await _repository.GetAllAsync(includes);
        }

        public async Task<Mahalle> GetByIdAsync(int id, params Expression<Func<Mahalle, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        public async Task<Mahalle> CreateMahalleAsync(Mahalle mahalle)
        {
            return await _repository.AddAsync(mahalle);
        }

        public async Task UpdateMahalleAsync(Mahalle mahalle)
        {
            await _repository.UpdateAsync(mahalle);
        }

        public async Task DeleteMahalleAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}