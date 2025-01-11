using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssuntosController : ControllerBase
    {
        private readonly IAssuntoBusiness _assuntoBusiness;

        public AssuntosController(IAssuntoBusiness assuntoBusiness)
        {
            _assuntoBusiness = assuntoBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var assuntos = await _assuntoBusiness.GetAllAsync();
            return Ok(assuntos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var assunto = await _assuntoBusiness.GetByIdAsync(id);
            if (assunto == null)
                return NotFound();
            return Ok(assunto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AssuntoRequest assunto)
        {
            var novoAssunto = await _assuntoBusiness.AddAsync(assunto);
            return CreatedAtAction(nameof(GetById), new { id = novoAssunto.Cod }, novoAssunto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AssuntoRequest assunto)
        {
            if (id != assunto.Cod)
                return BadRequest();

            var assuntoAtualizado = await _assuntoBusiness.UpdateAsync(assunto);
            return Ok(assuntoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _assuntoBusiness.DeleteAsync(id);
            return NoContent();
        }
    }

}
