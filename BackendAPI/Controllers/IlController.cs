using BackendAPI.Business.Abstract;
using BackendAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IlController : ControllerBase
    {
        private readonly IIlService _ilService;

        public IlController(IIlService ilService)
        {
            _ilService = ilService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Il>>> GetAll()
        {
            var iller = await _ilService.GetAllAsync(i => i.Ilceler);
            return Ok(iller);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Il>> GetById(int id)
        {
            var il = await _ilService.GetByIdAsync(id, i => i.Ilceler);
            if (il == null)
            {
                return NotFound();
            }
            return Ok(il);
        }

        //[HttpPost]
        //public async Task<ActionResult<Il>> Create(Il il)
        //{
        //    var createdIl = await _ilService.CreateIlAsync(il);
        //    return CreatedAtAction(nameof(GetById), new { id = createdIl.Id }, createdIl);
        //}

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Il il)
        {
            if (id != il.Id)
            {
                return BadRequest();
            }

            await _ilService.UpdateIlAsync(il);
            return NoContent();
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _ilService.DeleteIlAsync(id);
        //    return NoContent();
        //}
    }
}