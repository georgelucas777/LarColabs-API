using LarColabs.WebApi.Database;
using LarColabs.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LarColabs.WebApi.Services
{
    public class UsuarioService
    {
        private readonly LarColabsContext _context;
        private readonly IConfiguration _configuration;

        public UsuarioService(LarColabsContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<List<Usuario>> ObterTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> AdicionarAsync(Usuario usuario)
        {
            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
                throw new InvalidOperationException("E-mail já cadastrado para outro usuário.");

            if (await _context.Usuarios.AnyAsync(u => u.Cpf == usuario.Cpf))
                throw new InvalidOperationException("CPF já cadastrado para outro usuário.");

            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
            usuario.CriadoEm = DateTime.UtcNow;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> AtualizarAsync(int id, Usuario usuarioAtualizado)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return null;

            if (usuario.Email != usuarioAtualizado.Email &&
                await _context.Usuarios.AnyAsync(u => u.Email == usuarioAtualizado.Email && u.Id != id))
                throw new InvalidOperationException("E-mail já cadastrado para outro usuário.");

            if (usuario.Cpf != usuarioAtualizado.Cpf &&
                await _context.Usuarios.AnyAsync(u => u.Cpf == usuarioAtualizado.Cpf && u.Id != id))
                throw new InvalidOperationException("CPF já cadastrado para outro usuário.");

            usuario.Nome = usuarioAtualizado.Nome;
            usuario.Email = usuarioAtualizado.Email;
            usuario.Cpf = usuarioAtualizado.Cpf;
            usuario.Ativo = usuarioAtualizado.Ativo;
            usuario.AtualizadoEm = DateTime.UtcNow;

            if (!string.IsNullOrEmpty(usuarioAtualizado.Senha))
            {
                usuario.Senha = BCrypt.Net.BCrypt.HashPassword(usuarioAtualizado.Senha);
            }

            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LoginResponse?> LoginAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(senha, usuario.Senha))
                return null;

            if (!usuario.Ativo) return null;

            var log = new UsuarioLoginLog
            {
                UsuarioId = usuario.Id,
                DataHora = DateTime.UtcNow
            };
            _context.UsuarioLoginLogs.Add(log);
            await _context.SaveChangesAsync();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email, usuario.Email),
                    new Claim(ClaimTypes.Name, usuario.Nome)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new LoginResponse
            {
                Usuario = new UsuarioResponse
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email
                },
                Token = tokenString
            };
        }
    }

    public class LoginResponse
    {
        public UsuarioResponse Usuario { get; set; }
        public string Token { get; set; }
    }

    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
