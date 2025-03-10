using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;
using DataAccess.Concrete;
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

        public async Task UpdateeIlAsync(Il il)
        {
            await _repository.UpdateeAsync(il);
        }

        public async Task DeleteeIlAsync(int id)
        {
            await _repository.DeleteeAsync(id);
        }

        public async Task<IEnumerable<Ilce>> GetByIlIdAsync(int ilId, IlceRepository ýlceRepository)
        {
            return await ýlceRepository.GetAllAsync(i => i.IlId == ilId, i => i.Il, i => i.Mahalleler);
        }

    }
}