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
    public class ColaboradorController : ControllerBase
    {
        private readonly ColaboradorService _colaboradorService;
        private readonly ColaboradorTelefoneService _colaboradorTelefoneService;

        public ColaboradorController(
            ColaboradorService colaboradorService, 
            ColaboradorTelefoneService colaboradorTelefoneService)
        {
            _colaboradorService = colaboradorService;
            _colaboradorTelefoneService = colaboradorTelefoneService;
        }

        private int ObterUsuarioId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? int.Parse(userId) : 0;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> ObterTodos()
        {
            var colaboradores = await _colaboradorService.ObterTodosAsync();
            return Ok(colaboradores);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> ObterPorId(int id)
        {
            var colaborador = await _colaboradorService.ObterPorIdAsync(id);
            if (colaborador == null) return NotFound();
            return Ok(colaborador);
        }

        [HttpPost]
        public async Task<ActionResult<Colaborador>> Adicionar(Colaborador colaborador)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            var novoColaborador = await _colaboradorService.AdicionarAsync(colaborador, usuarioId);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoColaborador.Id }, novoColaborador);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Colaborador>> Atualizar(int id, Colaborador colaboradorAtualizado)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            var colaborador = await _colaboradorService.AtualizarAsync(id, colaboradorAtualizado, usuarioId);
            if (colaborador == null) return NotFound();
            return Ok(colaborador);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var sucesso = await _colaboradorService.RemoverAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpPost("{colaboradorId}/VincularTelefone/{telefoneId}")]
        public async Task<ActionResult> VincularTelefone(int colaboradorId, int telefoneId)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            var vinculo = await _colaboradorTelefoneService.VincularAsync(colaboradorId, telefoneId, usuarioId);
            if (vinculo == null) return BadRequest("Não foi possível criar o vínculo (já existe ou dados inválidos).");

            return CreatedAtAction(nameof(ObterPorId), new { id = colaboradorId }, new 
            {
                message = "Telefone vinculado com sucesso!",
                vinculo
            });
        }

        [HttpDelete("{colaboradorId}/DesvincularTelefone/{telefoneId}")]
        public async Task<ActionResult> DesvincularTelefone(int colaboradorId, int telefoneId)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            var sucesso = await _colaboradorTelefoneService.DesvincularAsync(colaboradorId, telefoneId, usuarioId);
            if (!sucesso) return NotFound("Vínculo não encontrado.");

            return Ok(new { message = "Telefone desvinculado com sucesso!" });
        }
    }
}
