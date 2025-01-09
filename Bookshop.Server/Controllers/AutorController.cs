using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorBusiness _autorBusiness;

        public AutorController(IAutorBusiness autorBusiness)
        {
            _autorBusiness = autorBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var autors = await _autorBusiness.GetAllAsync();
            return Ok(autors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _autorBusiness.GetByIdAsync(id);
            if (autor == null)
                return NotFound();
            return Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AutorRequest autor)
        {
            var novoAutor = await _autorBusiness.AddAsync(autor);
            return CreatedAtAction(nameof(GetById), new { id = novoAutor.Cod }, novoAutor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AutorRequest autor)
        {
            if (id != autor.Cod)
                return BadRequest();

            var autorAtualizado = await _autorBusiness.UpdateAsync(autor);
            return Ok(autorAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _autorBusiness.DeleteAsync(id);
            return NoContent();
        }
    }

}
