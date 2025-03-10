//using BackendAPI.Business.Abstract;
//using BackendAPI.Entities;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace BackendAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class MahalleController : ControllerBase
//    {
//        private readonly IMahalleService _mahalleService;

//        public MahalleController(IMahalleService mahalleService)
//        {
//            _mahalleService = mahalleService;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Mahalle>>> GetAll()
//        {
//            var mahalleler = await _mahalleService.GetAllAsync(m => m.Ilce, m => m.Ilce.Il);
//            return Ok(mahalleler);
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<Mahalle>> GetById(int id)
//        {
//            var mahalle = await _mahalleService.GetByIdAsync(id, m => m.Ilce, m => m.Ilce.Il);
//            if (mahalle == null)
//            {
//                return NotFound();
//            }
//            return Ok(mahalle);
//        }

//        //[HttpPost]
//        //public async Task<ActionResult<Mahalle>> Create(Mahalle mahalle)
//        //{
//        //    var createdMahalle = await _mahalleService.CreateMahalleAsync(mahalle);
//        //    return CreatedAtAction(nameof(GetById), new { id = createdMahalle.Id }, createdMahalle);
//        //}

//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(int id, Mahalle mahalle)
//        {
//            if (id != mahalle.Id)
//            {
//                return BadRequest();
//            }

//            await _mahalleService.UpdateMahalleAsync(mahalle);
//            return NoContent();
//        }

//        //[HttpDelete("{id}")]
//        //public async Task<IActionResult> Delete(int id)
//        //{
//        //    await _mahalleService.DeleteMahalleAsync(id);
//        //    return NoContent();
//        //}
//    }
//}





using BackendAPI.Business.Abstract;
using BackendAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahalleController : ControllerBase
    {
        private readonly IMahalleService _mahalleService;

        public MahalleController(IMahalleService mahalleService)
        {
            _mahalleService = mahalleService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetAll()
        {
            var mahalleler = await _mahalleService.GetAllAsync(m => m.Ilce, m => m.Ilce.Il);
            return Ok(mahalleler);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Mahalle>> GetById(int id)
        {
            var mahalle = await _mahalleService.GetByIdAsync(id, m => m.Ilce, m => m.Ilce.Il);
            if (mahalle == null)
            {
                return NotFound();
            }
            return Ok(mahalle);
        }

        [HttpGet("ilce/{ilceId}")]
        public async Task<ActionResult<IEnumerable<Mahalle>>> GetByIlceId(int ilceId)
        {
            var mahalleler = await _mahalleService.GetAllMahalleByIlceIdAsync(ilceId);

            if (mahalleler == null || !mahalleler.Any())
            {
                return NotFound("Bu ilçeye ait mahalle bulunamadý.");
            }

            return Ok(mahalleler);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Mahalle mahalle)
        {
            if (id != mahalle.Id)
            {
                return BadRequest();
            }

            await _mahalleService.UpdateeMahalleAsync(mahalle);
            return NoContent();
        }
    }
}
