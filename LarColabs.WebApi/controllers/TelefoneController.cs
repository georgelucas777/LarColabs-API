using LarColabs.WebApi.Models;
using LarColabs.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LarColabs.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TelefoneController : ControllerBase
    {
        private readonly TelefoneService _telefoneService;

        public TelefoneController(TelefoneService telefoneService)
        {
            _telefoneService = telefoneService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Telefone>>> ObterTodos()
        {
            var telefones = await _telefoneService.ObterTodosAsync();
            return Ok(telefones);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Telefone>> ObterPorId(int id)
        {
            var telefone = await _telefoneService.ObterPorIdAsync(id);
            if (telefone == null) return NotFound();
            return Ok(telefone);
        }

        [HttpPost]
        public async Task<ActionResult<Telefone>> Adicionar(Telefone telefone)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var novoTelefone = await _telefoneService.AdicionarAsync(telefone, usuarioId);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoTelefone.Id }, novoTelefone);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Telefone>> Atualizar(int id, Telefone telefoneAtualizado)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
            var telefone = await _telefoneService.AtualizarAsync(id, telefoneAtualizado, usuarioId);
            if (telefone == null) return NotFound();
            return Ok(telefone);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var sucesso = await _telefoneService.RemoverAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }
    }
}
