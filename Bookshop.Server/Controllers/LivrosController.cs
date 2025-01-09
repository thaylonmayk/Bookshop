using Bookshop.Domain.DTOs.Requests;
using Bookshop.Domain.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly ILivroBusiness _livroBusiness;

        public LivrosController(ILivroBusiness livroBusiness)
        {
            _livroBusiness = livroBusiness;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var livros = await _livroBusiness.GetAllAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var livro = await _livroBusiness.GetByIdAsync(id);
            if (livro == null)
                return NotFound();
            return Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LivroRequest livro)
        {
            var novoLivro = await _livroBusiness.AddAsync(livro);
            return CreatedAtAction(nameof(GetById), new { id = novoLivro.Cod }, novoLivro);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, LivroRequest livro)
        {
            if (id != livro.Cod)
                return BadRequest();

            var livroAtualizado = await _livroBusiness.UpdateAsync(livro);
            return Ok(livroAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _livroBusiness.DeleteAsync(id);
            return NoContent();
        }
    }

}
