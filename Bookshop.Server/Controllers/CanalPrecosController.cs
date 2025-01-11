using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanalPrecosController : ControllerBase
    {
        private readonly ICanalPrecoBusiness _canalPrecoBusiness;

        public CanalPrecosController(ICanalPrecoBusiness canalPrecoBusiness)
        {
            _canalPrecoBusiness = canalPrecoBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var canalPrecos = await _canalPrecoBusiness.GetAllAsync();
            return Ok(canalPrecos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var canalPreco = await _canalPrecoBusiness.GetByIdAsync(id);
            if (canalPreco == null)
                return NotFound();
            return Ok(canalPreco);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CanalPrecoRequest canalPreco)
        {
            var novoCanalPreco = await _canalPrecoBusiness.AddAsync(canalPreco);
            return CreatedAtAction(nameof(GetById), new { id = novoCanalPreco.Cod }, novoCanalPreco);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CanalPrecoRequest canalPreco)
        {
            if (id != canalPreco.Cod)
                return BadRequest();

            var canalPrecoAtualizado = await _canalPrecoBusiness.UpdateAsync(canalPreco);
            return Ok(canalPrecoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _canalPrecoBusiness.DeleteAsync(id);
            return NoContent();
        }
    }

}
