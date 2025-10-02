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
    public class TelefoneController : ControllerBase
    {
        private readonly TelefoneService _telefoneService;
        private readonly IMapper _mapper;

        public TelefoneController(TelefoneService telefoneService, IMapper mapper)
        {
            _telefoneService = telefoneService;
            _mapper = mapper;
        }

        private int ObterUsuarioId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return userId != null ? int.Parse(userId) : 0;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefoneReadDto>>> ObterTodos()
        {
            var telefones = await _telefoneService.ObterTodosAsync();
            var telefonesDto = _mapper.Map<IEnumerable<TelefoneReadDto>>(telefones);
            return Ok(telefonesDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TelefoneReadDto>> ObterPorId(int id)
        {
            var telefone = await _telefoneService.ObterPorIdAsync(id);
            if (telefone == null) return NotFound("Telefone não encontrado.");

            var telefoneDto = _mapper.Map<TelefoneReadDto>(telefone);
            return Ok(telefoneDto);
        }

        [HttpPost]
        public async Task<ActionResult<TelefoneReadDto>> Adicionar([FromBody] TelefoneCreateDto dto)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            try
            {
                var telefone = _mapper.Map<Telefone>(dto);
                var novoTelefone = await _telefoneService.AdicionarAsync(telefone, usuarioId);

                var telefoneDto = _mapper.Map<TelefoneReadDto>(novoTelefone);
                return CreatedAtAction(nameof(ObterPorId), new { id = telefoneDto.Id }, telefoneDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TelefoneReadDto>> Atualizar(int id, [FromBody] TelefoneUpdateDto dto)
        {
            var usuarioId = ObterUsuarioId();
            if (usuarioId == 0) return Unauthorized();

            try
            {
                var telefone = _mapper.Map<Telefone>(dto);
                var telefoneAtualizado = await _telefoneService.AtualizarAsync(id, telefone, usuarioId);

                if (telefoneAtualizado == null) return NotFound("Telefone não encontrado.");

                var telefoneDto = _mapper.Map<TelefoneReadDto>(telefoneAtualizado);
                return Ok(telefoneDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            try
            {
                await _telefoneService.RemoverAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
