using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;
using Microsoft.EntityFrameworkCore;


namespace BackendAPI.Business.Concrete
{

    public class TasinmazService : ITasinmazService
    {
        private readonly AppDbContext _dbContext;
        private readonly IRepository<Tasinmaz> _repository;

        //  İki constructor yerine TEK BİR constructor kullanıyoruz!
        public TasinmazService(AppDbContext dbContext, IRepository<Tasinmaz> repository)
        {
            _dbContext = dbContext;
            _repository = repository;
        }

        public async Task<IEnumerable<Tasinmaz>> GetAllAsync(Expression<Func<Tasinmaz, object>>[] includes = null)
        {
            return await _repository.GetAllAsync(includes);
        }

        public async Task<Tasinmaz> GetByIdAsync(int id, Expression<Func<Tasinmaz, object>>[] includes = null)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        public async Task<Tasinmaz> CreateTasinmazAsync(Tasinmaz tasinmaz)
        {
            return await _repository.AddAsync(tasinmaz);
        }

        public async Task UpdateTasinmazAsync(Tasinmaz tasinmaz)
        {
            await _repository.UpdateAsync(tasinmaz);
        }

        public async Task DeleteTasinmazAsync(int id)
        {
            var entity = await _dbContext.Tasinmazlar.FindAsync(id);

            if (entity == null)
            {
                throw new KeyNotFoundException("Belirtilen taşınmaz bulunamadı.");
            }

            _dbContext.Tasinmazlar.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteTasinmazAsync(List<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return false;
            }

            var entities = await _dbContext.Tasinmazlar
                                           .Where(t => ids.Contains(t.Id))
                                           .ToListAsync();

            if (!entities.Any())
            {
                return false;
            }

            _dbContext.Tasinmazlar.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }

}