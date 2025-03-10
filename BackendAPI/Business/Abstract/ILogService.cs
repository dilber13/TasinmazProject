using System.Collections.Generic;
using System.Threading.Tasks;
using BackendAPI.Entities;




namespace BackendAPI.Business.Abstract
{
    public interface ILogService
    {
        Task<List<Log>> GetLogsAsync(bool? durum);
        Task<Log> LogAsync(bool durum, string islemTipi, string aciklama, string userIp, int? userId);

    }
}
