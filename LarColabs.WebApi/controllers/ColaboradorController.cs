using AutoMapper;
using LarColabs.WebApi.DTOs;
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
        private readonly IMapper _mapper;

        public ColaboradorController(
            ColaboradorService colaboradorService, 
            ColaboradorTelefoneService colaboradorTelefoneService,
            IMapper mapper)
        {
            _colaboradorService = colaboradorService;
            _colaboradorTelefoneService = colaboradorTelefoneService;
            _mapper = mapper;
        }

        private int ObterUsuarioId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? int.Parse(userId) : 0;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColaboradorReadDto>>> ObterTodos()
        {
            var colaboradores = await _colaboradorService.ObterTodosAsync();
            var colaboradoresDto = _mapper.Map<IEnumerable<ColaboradorReadDto>>(colaboradores);
            return Ok(colaboradoresDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ColaboradorReadDto>> ObterPorId(int id)
        {
            var colaborador = await _colaboradorService.ObterPorIdAsync(id);
            if (colaborador == null) return NotFound();

            var colaboradorDto = _mapper.Map<ColaboradorReadDto>(colaborador);
            return Ok(colaboradorDto);
        }

        [HttpPost]
        public async Task<ActionResult<ColaboradorReadDto>> Adicionar([FromBody] ColaboradorCreateDto dto)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            var colaborador = _mapper.Map<Colaborador>(dto);
            var novoColaborador = await _colaboradorService.AdicionarAsync(colaborador, usuarioId);

            var colaboradorDto = _mapper.Map<ColaboradorReadDto>(novoColaborador);
            return CreatedAtAction(nameof(ObterPorId), new { id = colaboradorDto.Id }, colaboradorDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ColaboradorReadDto>> Atualizar(int id, [FromBody] ColaboradorUpdateDto dto)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            var colaborador = _mapper.Map<Colaborador>(dto);
            var colaboradorAtualizado = await _colaboradorService.AtualizarAsync(id, colaborador, usuarioId);

            if (colaboradorAtualizado == null) return NotFound();

            var colaboradorDto = _mapper.Map<ColaboradorReadDto>(colaboradorAtualizado);
            return Ok(colaboradorDto);
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

            try
            {
                var vinculo = await _colaboradorTelefoneService.VincularAsync(colaboradorId, telefoneId, usuarioId);

                return Ok(new
                {
                    message = "Telefone vinculado com sucesso!",
                    vinculo
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{colaboradorId}/DesvincularTelefone/{telefoneId}")]
        public async Task<ActionResult> DesvincularTelefone(int colaboradorId, int telefoneId)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            try
            {
                await _colaboradorTelefoneService.DesvincularAsync(colaboradorId, telefoneId, usuarioId);
                return Ok(new { message = "Telefone desvinculado com sucesso!" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
