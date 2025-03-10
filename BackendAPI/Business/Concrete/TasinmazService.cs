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
        private readonly ILogService _logService; // Doğru değişken (_logService)

        // Constructor içine LogService eklenmeli
        public TasinmazService(AppDbContext dbContext, IRepository<Tasinmaz> repository, ILogService logService)
        {
            _dbContext = dbContext;
            _repository = repository;
            _logService = logService ?? throw new ArgumentNullException(nameof(logService)); // Atama yapıldı
        }

        public async Task<IEnumerable<Tasinmaz>> GetAllAsync(Expression<Func<Tasinmaz, object>>[] includes = null)
        {
            return await _repository.GetAllAsync(includes);
        }

        public async Task<Tasinmaz> GetByIdAsync(int id, Expression<Func<Tasinmaz, object>>[] includes = null)
        {
            return await _repository.GetByIdAsync(id, includes);
        }

        //public async Task<Tasinmaz> CreateTasinmazAsync(Tasinmaz tasinmaz)
        //{
        //    return await _repository.AddAsync(tasinmaz);
        //}

        //public async Task UpdateTasinmazAsync(Tasinmaz tasinmaz)
        //{
        //    await _repository.UpdateAsync(tasinmaz);
        //}

        //public async Task DeleteTasinmazAsync(int id)
        //{
        //    var entity = await _dbContext.Tasinmazlar.FindAsync(id);

        //    if (entity == null)
        //    {
        //        throw new KeyNotFoundException("Belirtilen taşınmaz bulunamadı.");
        //    }

        //    _dbContext.Tasinmazlar.Remove(entity);
        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteTasinmazAsync(List<int> ids)
        //{
        //    if (ids == null || !ids.Any())
        //    {
        //        return false;
        //    }

        //    var entities = await _dbContext.Tasinmazlar
        //                                   .Where(t => ids.Contains(t.Id))
        //                                   .ToListAsync();

        //    if (!entities.Any())
        //    {
        //        return false;
        //    }

        //    _dbContext.Tasinmazlar.RemoveRange(entities);
        //    await _dbContext.SaveChangesAsync();
        //    return true;
        //}
















        public async Task<List<Tasinmaz>> GetTasinmazByUserIdAsync(int userId)
        {
            return await _dbContext.Tasinmazlar
                .Where(t => t.userId == userId)
                .Include(t => t.Mahalle)
                .ThenInclude(m => m.Ilce)
                .ThenInclude(i => i.Il)
                .ToListAsync();
        }



        public async Task<bool> DeleteeTasinmazAsync(List<int> ids, int userId)
        {
            var tasinmazlar = await _repository.GetAllAsync(t => ids.Contains(t.Id));

            if (!tasinmazlar.Any())
            {
                await _logService.LogAsync(
                    false,
                    "Silme",
                    $"Hiçbir taşınmaz silinemedi. IDs: {string.Join(", ", ids)}",
                    "127.0.0.1",
                    userId
                );
                return false;
            }

            await _repository.DeleteeAsync(tasinmazlar);
            await _logService.LogAsync(
                true,
                "Silme",
                $"Seçili taşınmazlar ({string.Join(", ", ids)}) başarıyla silindi.",
                "127.0.0.1",
                userId
            );

            return true;
        }



        public async Task<Tasinmaz> UpdateeTasinmazAsync(Tasinmaz tasinmaz, int userId)
        {

            if (userId <= 0)
            {
                throw new ArgumentException("Geçersiz kullanıcı ID.");
            }


            try
            {

                var existingTasinmaz = await _dbContext.Tasinmazlar.FindAsync(tasinmaz.Id);

                if (existingTasinmaz == null)
                {


                    await _logService.LogAsync(
                        false,
                        "Güncelleme",
                        $"Taşınmaz (ID: {tasinmaz.Id}) bulunamadı.",
                        "127.0.0.1",
                        userId
                    );
                    return null;
                }

                existingTasinmaz.Ada = tasinmaz.Ada;
                existingTasinmaz.Parsel = tasinmaz.Parsel;
                existingTasinmaz.Nitelik = tasinmaz.Nitelik;
                existingTasinmaz.Adres = tasinmaz.Adres;
                existingTasinmaz.MahalleId = tasinmaz.MahalleId;

                await _dbContext.SaveChangesAsync();

                await _logService.LogAsync(
                    true,
                    "Güncelleme",
                    $"Taşınmaz (ID: {tasinmaz.Id}) başarıyla güncellendi.",
                    "127.0.0.1",
                    userId
                );

                var updatedTasinmaz = await _dbContext.Tasinmazlar
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Il)
                    .FirstOrDefaultAsync(t => t.Id == tasinmaz.Id);

                return updatedTasinmaz;
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    false,
                    "Güncelleme",
                    $"Hata oluştu: {ex.Message}",
                    "127.0.0.1",
                    tasinmaz.userId
                );
                throw;
            }
        }


        public async Task<Tasinmaz> CreateTasinmazAsync(Tasinmaz tasinmaz)
        {

            if (tasinmaz.userId <= 0)
            {
                throw new ArgumentException("Geçersiz kullanıcı ID.");
            }

            try
            {

                await _dbContext.Tasinmazlar.AddAsync(tasinmaz);
                await _dbContext.SaveChangesAsync();

                // Log kaydı
                await _logService.LogAsync(
                    true,
                    "Ekleme",
                    $"Taşınmaz (ID: {tasinmaz.Id}) başarıyla eklendi.",
                    "127.0.0.1",
                    tasinmaz.userId
                );

                var addedTasinmaz = await _dbContext.Tasinmazlar
                    .Include(t => t.Mahalle)
                        .ThenInclude(m => m.Ilce)
                            .ThenInclude(i => i.Il)
                    .FirstOrDefaultAsync(t => t.Id == tasinmaz.Id);

                return addedTasinmaz;
            }
            catch (Exception ex)
            {
                await _logService.LogAsync(
                    false,
                    "Ekleme",
                    $"Hata oluştu: {ex.Message}",
                    "127.0.0.1",
                    tasinmaz.userId
                );
                throw;
            }
        }


    }

}