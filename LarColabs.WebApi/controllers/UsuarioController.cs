using AutoMapper;
using LarColabs.WebApi.DTOs;
using LarColabs.WebApi.Models;
using LarColabs.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LarColabs.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IMapper _mapper;

        public UsuarioController(UsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDto>>> ObterTodos()
        {
            var usuarios = await _usuarioService.ObterTodosAsync();
            return Ok(_mapper.Map<IEnumerable<UsuarioReadDto>>(usuarios));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioReadDto>> ObterPorId(int id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null) return NotFound();

            return Ok(_mapper.Map<UsuarioReadDto>(usuario));
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioReadDto>> Registrar([FromBody] UsuarioCreateDto dto)
        {
            try
            {
                var usuario = _mapper.Map<Usuario>(dto);
                usuario.Ativo = true;

                var novoUsuario = await _usuarioService.AdicionarAsync(usuario);

                return CreatedAtAction(nameof(ObterPorId),
                    new { id = novoUsuario.Id },
                    _mapper.Map<UsuarioReadDto>(novoUsuario));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var (response, error) = await _usuarioService.LoginAsync(request.Email, request.Senha);

            if (response == null)
                return Unauthorized(new { message = error });

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<UsuarioReadDto>> Atualizar(int id, [FromBody] UsuarioUpdateDto dto)
        {
            try
            {
                var usuarioAtualizado = await _usuarioService.AtualizarAsync(id, _mapper.Map<Usuario>(dto));
                if (usuarioAtualizado == null) return NotFound();

                return Ok(_mapper.Map<UsuarioReadDto>(usuarioAtualizado));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var sucesso = await _usuarioService.RemoverAsync(id);
            if (!sucesso) return NotFound();

            return NoContent();
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
