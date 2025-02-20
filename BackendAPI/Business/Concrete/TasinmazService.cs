using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;

namespace BackendAPI.Business.Concrete
{

    public class TasinmazService : ITasinmazService
    {

        /*
         TasinmazService sınıfı, Tasinmaz nesneleriyle ilgili işlemleri gerçekleştirmek için tasarlanmıştır.
         Bu sınıf ITasinmazService interface'ini uygular.
         IRepository<Tasinmaz> interface'ini uygulayan Repository sınıfını kullanarak veritabanı işlemlerini gerçekleştirir.
        */
        private readonly IRepository<Tasinmaz> _repository;

        public TasinmazService(IRepository<Tasinmaz> repository)
        {
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
            await _repository.DeleteAsync(id);
        }
    }
}