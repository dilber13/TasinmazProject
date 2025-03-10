using Microsoft.AspNetCore.Mvc;
using global::BackendAPI.Business.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasinmazController : ControllerBase
    {
        private readonly ITasinmazService _tasinmazService;

        public TasinmazController(ITasinmazService tasinmazService)
        {
            _tasinmazService = tasinmazService;
        }

        // GET: api/Tasinmaz
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasinmaz>>> GetTasinmazlar()
        {
            var tasinmazlar = await _tasinmazService.GetAllAsync(
                t => t.Mahalle,
                t => t.Mahalle.Ilce,
                t => t.Mahalle.Ilce.Il
            );
            /*
            Eger mahalleleri de istiyorsak
            var tasinmazlar = await _tasinmazService.GetAllAsync(
                t => t.Include(t => t.Mahalle)
                .Include(t => t.Mahalle.Ilce)
                .Include(t => t.Mahalle.Ilce.Il)
            );
            */
            return Ok(tasinmazlar);
        }

        // GET: api/Tasinmaz/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tasinmaz>> GetTasinmaz(int id)
        {


            var tasinmaz = await _tasinmazService.GetByIdAsync(id,
                t => t.Mahalle,
                t => t.Mahalle.Ilce,
                t => t.Mahalle.Ilce.Il,
                t => t.User
            );

            if (tasinmaz == null)
            {
                return NotFound();
            }

            return Ok(tasinmaz);
        }

        // POST: api/Tasinmaz
        //[HttpPost]
        //public async Task<ActionResult<Tasinmaz>> CreateTasinmaz(Tasinmaz tasinmaz)
        //{
        //    var createdTasinmaz = await _tasinmazService.CreateTasinmazAsync(tasinmaz);
        //    return CreatedAtAction(nameof(GetTasinmaz), new { id = createdTasinmaz.Id }, createdTasinmaz);
        //}

        // PUT: api/Tasinmaz/5

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTasinmaz(int id, [FromBody] Tasinmaz tasinmaz)
        //{
        //    try
        //    {
        //        if (tasinmaz == null)
        //        {
        //            return BadRequest("Geçersiz taşınmaz verisi.");
        //        }

        //        if (id != tasinmaz.Id)
        //        {
        //            return BadRequest("ID uyuşmazlığı.");
        //        }

        //        await _tasinmazService.UpdateTasinmazAsync(tasinmaz);

        //        return NoContent(); // Güncelleme başarılı, içerik yok.
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Console.WriteLine($"Update error: {ex.Message}");
        //        System.Console.WriteLine($"StackTrace: {ex.StackTrace}");
        //        return StatusCode(500, $"Güncelleme sırasında hata oluştu: {ex.Message}");
        //    }
        //}



        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateTasinmaz(int id, Tasinmaz tasinmaz)
        //{
        //    if (id != tasinmaz.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await _tasinmazService.UpdateTasinmazAsync(tasinmaz);
        //    return NoContent();
        //}

        // DELETE: api/Tasinmaz/5

        //[HttpDelete("delete")]
        //public async Task<IActionResult> DeleteTasinmaz([FromBody] List<int> ids)
        //{
        //    if (ids == null || ids.Count == 0)
        //    {
        //        return BadRequest("Silinecek ID listesi boş.");
        //    }

        //    bool deleted = await _tasinmazService.DeleteTasinmazAsync(ids);

        //    if (!deleted)
        //    {
        //        return NotFound("Bazı veya tüm taşınmazlar bulunamadı ya da silinemedi.");
        //    }

        //    return Ok(new { message = "Seçili taşınmazlar başarıyla silindi." });
        //}



        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTasinmaz(int id)
        //{
        //    await _tasinmazService.DeleteTasinmazAsync(id);
        //    return NoContent();
        //}












        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetTasinmazByUserId(int userId)
        {
            var tasinmazlar = await _tasinmazService.GetTasinmazByUserIdAsync(userId);

            if (tasinmazlar == null || tasinmazlar.Count == 0)
            {
                return NotFound("Bu id ye ait tasinmaz bulunamadı.");
            }

            return Ok(tasinmazlar);
        }

        [Authorize]

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteeTasinmaz([FromBody] List<int> ids)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            if (userId == 0)
            {
                return Unauthorized("Kullanıcı kimliği alınamadı.");
            }

            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Silinecek ID listesi boş.");
            }

            var result = await _tasinmazService.DeleteeTasinmazAsync(ids, userId);

            if (!result)
            {
                return BadRequest("Bazı kayıtlar silinirken bir hata oluştu.");
            }

            return Ok(new { message = "Seçili taşınmazlar başarıyla silindi." });
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateeTasinmaz(int id, [FromBody] Tasinmaz tasinmaz)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                var userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

                if (userId == 0)
                {
                    return Unauthorized("Kullanıcı kimliği alınamadı.");
                }

                var updatedTasinmaz = await _tasinmazService.UpdateeTasinmazAsync(tasinmaz, userId);

                if (updatedTasinmaz == null)
                {
                    return NotFound("Taşınmaz bulunamadı.");
                }

                return Ok(updatedTasinmaz);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, $"Güncelleme sırasında hata oluştu: {ex.Message}");
            }
        }


        [HttpPost("add")]
        public async Task<IActionResult> CreateTasinmaz([FromBody] Tasinmaz tasinmaz)
        {
            try
            {
                Console.WriteLine($"Auth Header: {Request.Headers["Authorization"]}");

                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Console.WriteLine($"User ID Claim: {userIdClaim ?? "null"}");

                if (string.IsNullOrEmpty(userIdClaim))
                {
                    Console.WriteLine("Unauthorized: User ID claim bulunamadı");
                    return Unauthorized("Kullanıcı oturum açmamış.");
                }

                if (int.TryParse(userIdClaim, out int userId))
                {
                    tasinmaz.userId = userId;
                }
                else
                {
                    Console.WriteLine($"Geçersiz user ID formatı: {userIdClaim}");
                    return BadRequest("Geçersiz kullanıcı ID'si.");
                }

                var result = await _tasinmazService.CreateTasinmazAsync(tasinmaz);
                if (result == null)
                {
                    Console.WriteLine("Taşınmaz ekleme başarısız");
                    return BadRequest("Taşınmaz eklenirken bir hata oluştu.");
                }

                return CreatedAtAction(nameof(GetTasinmaz), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
