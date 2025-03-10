//namespace BackendAPI.DataAccess.Abstract
//{
//    public interface IIlceRepository
//    {
//    }
//}



using System.Collections.Generic;
using System.Threading.Tasks;
using BackendAPI.DataAccess.Abstract;
using BackendAPI.Entities;


namespace DataAccess.Abstract
{
    public interface IIlceRepository : IRepository<Ilce>
    {
        Task<IEnumerable<Ilce>> GetByIlIdAsync(int ilId);
    }
}
