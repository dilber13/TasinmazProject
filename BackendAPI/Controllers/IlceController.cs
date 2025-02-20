using BackendAPI.Business.Abstract;
using BackendAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlceController : ControllerBase
    {
        private readonly IIlceService _ilceService;

        public IlceController(IIlceService ilceService)
        {
            _ilceService = ilceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ilce>>> GetAll()
        {
            var ilceler = await _ilceService.GetAllAsync(i => i.Il, i => i.Mahalleler);
            return Ok(ilceler);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ilce>> GetById(int id)
        {
            var ilce = await _ilceService.GetByIdAsync(id, i => i.Il, i => i.Mahalleler);
            if (ilce == null)
            {
                return NotFound();
            }
            return Ok(ilce);
        }

        //[HttpPost]
        //public async Task<ActionResult<Ilce>> Create(Ilce ilce)
        //{
        //    var createdIlce = await _ilceService.CreateIlceAsync(ilce);
        //    return CreatedAtAction(nameof(GetById), new { id = createdIlce.Id }, createdIlce);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Ilce ilce)
        {
            if (id != ilce.Id)
            {
                return BadRequest();
            }

            await _ilceService.UpdateIlceAsync(ilce);
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _ilceService.DeleteIlceAsync(id);
        //    return NoContent();
        //}
    }
}