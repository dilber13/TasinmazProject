using Microsoft.AspNetCore.Mvc;
using global::BackendAPI.Business.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackendAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;


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
                t => t.Mahalle.Ilce.Il
            );

            if (tasinmaz == null)
            {
                return NotFound();
            }

            return Ok(tasinmaz);
        }

        // POST: api/Tasinmaz
        [HttpPost]
        public async Task<ActionResult<Tasinmaz>> CreateTasinmaz(Tasinmaz tasinmaz)
        {
            var createdTasinmaz = await _tasinmazService.CreateTasinmazAsync(tasinmaz);
            return CreatedAtAction(nameof(GetTasinmaz), new { id = createdTasinmaz.Id }, createdTasinmaz);
        }

        // PUT: api/Tasinmaz/5

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTasinmaz(int id, [FromBody] Tasinmaz tasinmaz)
        {
            try
            {
                if (tasinmaz == null)
                {
                    return BadRequest("Geçersiz taşınmaz verisi.");
                }

                if (id != tasinmaz.Id)
                {
                    return BadRequest("ID uyuşmazlığı.");
                }

                await _tasinmazService.UpdateTasinmazAsync(tasinmaz);

                return NoContent(); // Güncelleme başarılı, içerik yok.
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Update error: {ex.Message}");
                System.Console.WriteLine($"StackTrace: {ex.StackTrace}");
                return StatusCode(500, $"Güncelleme sırasında hata oluştu: {ex.Message}");
            }
        }



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

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTasinmaz([FromBody] List<int> ids)
        {
            if (ids == null || ids.Count == 0)
            {
                return BadRequest("Silinecek ID listesi boş.");
            }

            bool deleted = await _tasinmazService.DeleteTasinmazAsync(ids);

            if (!deleted)
            {
                return NotFound("Bazı veya tüm taşınmazlar bulunamadı ya da silinemedi.");
            }

            return Ok(new { message = "Seçili taşınmazlar başarıyla silindi." });
        }



        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTasinmaz(int id)
        //{
        //    await _tasinmazService.DeleteTasinmazAsync(id);
        //    return NoContent();
        //}
    }
}
