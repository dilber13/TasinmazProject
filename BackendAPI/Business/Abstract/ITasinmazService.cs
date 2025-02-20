using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Entities;
using System.Linq.Expressions;

namespace BackendAPI.Business.Abstract
{

    /*
        TasinmazService interface'i, Tasinmaz nesneleriyle ilgili işlemleri gerçekleştirmek için tasarlanmıştır.
        Bu interface, Tasinmaz nesneleriyle ilgili CRUD işlemlerini içerir. Ekstra işlemler için de metotlar ekleyebilirsiniz.
    */

    public interface ITasinmazService
    {
        Task<IEnumerable<Tasinmaz>> GetAllAsync(params Expression<Func<Tasinmaz, object>>[] includes);
        Task<Tasinmaz> GetByIdAsync(int id, params Expression<Func<Tasinmaz, object>>[] includes);
        Task<Tasinmaz> CreateTasinmazAsync(Tasinmaz tasinmaz);
        Task UpdateTasinmazAsync(Tasinmaz tasinmaz);
        Task DeleteTasinmazAsync(int id);
    }
}