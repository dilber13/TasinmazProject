using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendAPI.Business.Concrete
{
    public class IlService : IIlService
    {
        private readonly IRepository<Il> _repository;

        public IlService(IRepository<Il> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Il>> GetAllAsync(params Expression<Func<Il, object>>[] includes)
        {
            return await _repository.GetAllAsync(includes);
        }

        public async Task<Il> GetByIdAsync(int id, params Expression<Func<Il, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        public async Task<Il> CreateIlAsync(Il il)
        {
            return await _repository.AddAsync(il);
        }

        public async Task UpdateIlAsync(Il il)
        {
            await _repository.UpdateAsync(il);
        }

        public async Task DeleteIlAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}