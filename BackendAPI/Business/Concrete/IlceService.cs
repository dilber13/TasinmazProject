//using BackendAPI.Business.Abstract;
//using BackendAPI.DataAccess.Abstract;
//using BackendAPI.Entities;
//using DataAccess.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Threading.Tasks;

//namespace BackendAPI.Business.Concrete
//{
//    public class IlceService : IIlceService
//    {
//        private readonly IRepository<Ilce> _repository;


//        private readonly IIlceRepository _ilceRepository;

//        public IlceService(IIlceRepository ilceRepository)
//        {
//            _ilceRepository = ilceRepository;
//        }

//        public async Task<IEnumerable<Ilce>> GetByIlIdAsync(int ilId)
//        {
//            return await _ilceRepository.GetByIlIdAsync(ilId);
//        }

//        public IlceService(IRepository<Ilce> repository)
//        {
//            _repository = repository;
//        }

//        public async Task<IEnumerable<Ilce>> GetAllAsync(params Expression<Func<Ilce, object>>[] includes)
//        {
//            return await _repository.GetAllAsync(includes);
//        }

//        public async Task<Ilce> GetByIdAsync(int id, params Expression<Func<Ilce, object>>[] includes)
//        {
//            return await _repository.GetByIdAsync(id, includes);
//        }

//        public async Task<Ilce> CreateIlceAsync(Ilce ilce)
//        {
//            return await _repository.AddAsync(ilce);
//        }

//        public async Task UpdateIlceAsync(Ilce ilce)
//        {
//            await _repository.UpdateAsync(ilce);
//        }

//        public async Task DeleteIlceAsync(int id)
//        {
//            await _repository.DeleteAsync(id);
//        }
//    }
//}





using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BackendAPI.Business.Concrete
{
    public class IlceService : IIlceService
    {
        private readonly IRepository<Ilce> _repository;
        private readonly IIlceRepository _ilceRepository;

        // Tek bir constructor kullanarak dependency injection çözüldü
        public IlceService(IIlceRepository ilceRepository, IRepository<Ilce> repository)
        {
            _ilceRepository = ilceRepository;
            _repository = repository;
        }

        public async Task<IEnumerable<Ilce>> GetByIlIdAsync(int ilId)
        {
            return await _ilceRepository.GetByIlIdAsync(ilId);
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

        public async Task UpdateeIlceAsync(Ilce ilce)
        {
            await _repository.UpdateeAsync(ilce);
        }

        public async Task DeleteeIlceAsync(int id)
        {
            await _repository.DeleteeAsync(id);
        }
    }
}
