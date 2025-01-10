using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanalsController : ControllerBase
    {
        private readonly ICanalBusiness _canalBusiness;

        public CanalsController(ICanalBusiness canalBusiness)
        {
            _canalBusiness = canalBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var canals = await _canalBusiness.GetAllAsync();
            return Ok(canals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var canal = await _canalBusiness.GetByIdAsync(id);
            if (canal == null)
                return NotFound();
            return Ok(canal);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CanalRequest canal)
        {
            var novoCanal = await _canalBusiness.AddAsync(canal);
            return CreatedAtAction(nameof(GetById), new { id = novoCanal.Cod }, novoCanal);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CanalRequest canal)
        {
            if (id != canal.Cod)
                return BadRequest();

            var canalAtualizado = await _canalBusiness.UpdateAsync(canal);
            return Ok(canalAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _canalBusiness.DeleteAsync(id);
            return NoContent();
        }
    }

}
