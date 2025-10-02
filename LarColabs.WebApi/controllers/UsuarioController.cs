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

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObterTodos()
        {
            var usuarios = await _usuarioService.ObterTodosAsync();
            return Ok(usuarios);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObterPorId(int id)
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<Usuario>> Registrar(Usuario usuario)
        {
            var novoUsuario = await _usuarioService.AdicionarAsync(usuario);
            return CreatedAtAction(nameof(ObterPorId), new { id = novoUsuario.Id }, novoUsuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var response = await _usuarioService.LoginAsync(request.Email, request.Senha);
            if (response == null)
                return Unauthorized("Credenciais inválidas");

            return Ok(response);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Atualizar(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _usuarioService.AtualizarAsync(id, usuarioAtualizado);
            if (usuario == null) return NotFound();
            return Ok(usuario);
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
