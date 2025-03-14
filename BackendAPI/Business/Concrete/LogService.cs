﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendAPI.Business.Abstract;
using BackendAPI.DataAccess;
using BackendAPI.Entities;



namespace BackendAPI.Business.Concrete
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;
        public LogService(AppDbContext context)
        {
            _context = context;
        }


        
        public async Task<List<Log>> GetLogsAsync(bool? durum)
        {
            var query = _context.Logs.AsQueryable();

            if (durum.HasValue)
            {
                query = query.Where(l => l.durum == durum.Value);
            }

            return await query
                .OrderByDescending(l => l.zaman)  // En son loglar önce gelsin
                .ToListAsync();
        }

        public async Task<Log> LogAsync(bool durum, string islemTipi, string aciklama, string userIp, int? userId)
        {
            try
            {
                var log = new Log
                {
                    durum = durum,
                    islemTipi = islemTipi,
                    aciklama = aciklama,
                    userIp = userIp,
                    userId = userId,
                    zaman = DateTime.Now
                };

                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
                return log;
            }
            catch (Exception ex)
            {
                // Exception durumunda yeni bir log kaydı oluştur
                var errorLog = new Log
                {
                    durum = false,
                    islemTipi = islemTipi,
                    aciklama = $"Log kaydedilirken hata oluştu: {ex.Message}",
                    userIp = userIp,
                    userId = userId,
                    zaman = DateTime.Now
                };

                try
                {
                    await _context.Logs.AddAsync(errorLog);
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    // Eğer error log kaydı da başarısız olursa en azından console'a yazalım
                    Console.WriteLine($"Log kaydedilirken kritik hata oluştu: {ex.Message}");
                }

                throw; // Orijinal hatayı fırlat
            }
        }



    }
}
