//using BackendAPI.Business.Abstract;
//using BackendAPI.DataAccess.Abstract;
//using BackendAPI.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace BackendAPI.Business.Concrete
//{
//    public class MahalleService : IMahalleService
//    {
//        private readonly IRepository<Mahalle> _repository;

//        public MahalleService(IRepository<Mahalle> repository)
//        {
//            _repository = repository;
//        }

//        public async Task<IEnumerable<Mahalle>> GetAllAsync(params Expression<Func<Mahalle, object>>[] includes)
//        {
//            return await _repository.GetAllAsync(includes);
//        }

//        public async Task<Mahalle> GetByIdAsync(int id, params Expression<Func<Mahalle, object>>[] includes)
//        {
//            return await _repository.GetByIdAsync(id, includes);
//        }

//        public async Task<Mahalle> CreateMahalleAsync(Mahalle mahalle)
//        {
//            return await _repository.AddAsync(mahalle);
//        }

//        public async Task UpdateMahalleAsync(Mahalle mahalle)
//        {
//            await _repository.UpdateAsync(mahalle);
//        }

//        public async Task DeleteMahalleAsync(int id)
//        {
//            await _repository.DeleteAsync(id);
//        }
//    }
//}



using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackendAPI.DataAccess;

namespace BackendAPI.Business.Concrete
{
    public class MahalleService : IMahalleService
    {
        private readonly IRepository<Mahalle> _repository;
        private readonly AppDbContext _context; // DbContext'i direkt kullanmak için ekledim.

        public MahalleService(IRepository<Mahalle> repository, AppDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<Mahalle>> GetAllAsync(Expression<Func<Mahalle, bool>> filter = null, params Expression<Func<Mahalle, object>>[] includes)
        {
            return await _repository.GetAllAsync(filter, includes);
        }

        public async Task<Mahalle> GetByIdAsync(int id, params Expression<Func<Mahalle, object>>[] includes)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        public async Task<Mahalle> CreateMahalleAsync(Mahalle mahalle)
        {
            return await _repository.AddAsync(mahalle);
        }

        public async Task UpdateeMahalleAsync(Mahalle mahalle)
        {
            await _repository.UpdateeAsync(mahalle);
        }

        public async Task DeleteeMahalleAsync(int id)
        {
            await _repository.DeleteeAsync(id);
        }

        /// <summary>
        /// IlceId'ye göre mahalleleri getirir.
        /// </summary>
        public async Task<IEnumerable<Mahalle>> GetAllMahalleByIlceIdAsync(int ilceId)
        {
            return await _context.Mahalleler
                .Where(m => m.IlceId == ilceId)
                .Include(m => m.Ilce)        // Ilçe bilgisini yükle
                .ThenInclude(ilce => ilce.Il) // Ýl bilgisini de yükle
                .ToListAsync();
        }

        public Task<IEnumerable<Mahalle>> GetAllAsync(params Expression<Func<Mahalle, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
