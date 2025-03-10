//namespace BackendAPI.DataAccess.Concrete
//{
//    public class IlceRepository
//    {
//    }
//}



using System.Collections.Generic;
using System.Threading.Tasks;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using BackendAPI.DataAccess.Concrete;
using BackendAPI.DataAccess;
using BackendAPI.Entities;
using System.Linq;

namespace DataAccess.Concrete
{
    public class IlceRepository : Repository<Ilce>, IIlceRepository
    {
        private readonly AppDbContext _context;

        public IlceRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ilce>> GetByIlIdAsync(int ilId)
        {
            return await _context.Ilceler
                                 .Include(i => i.Il)
                                 .Include(i => i.Mahalleler)
                                 .Where(i => i.IlId == ilId)
                                 .ToListAsync();
        }
    }
}
