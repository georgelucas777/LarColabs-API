using LarColabs.WebApi.Database;
using LarColabs.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LarColabs.WebApi.Services
{
    public class ColaboradorTelefoneService
    {
        private readonly LarColabsContext _context;

        public ColaboradorTelefoneService(LarColabsContext context)
        {
            _context = context;
        }

        public async Task<ColaboradorTelefone?> VincularAsync(int colaboradorId, int telefoneId, int usuarioId)
        {
            var existe = await _context.ColaboradoresTelefones
                .FirstOrDefaultAsync(ct => ct.ColaboradorId == colaboradorId && ct.TelefoneId == telefoneId);

            if (existe != null) return existe;

            var vinculo = new ColaboradorTelefone
            {
                ColaboradorId = colaboradorId,
                TelefoneId = telefoneId,
                CriadoPor = usuarioId,
                CriadoEm = DateTime.UtcNow
            };

            _context.ColaboradoresTelefones.Add(vinculo);

            var log = new ColaboradorTelefoneLog
            {
                ColaboradorId = colaboradorId,
                TelefoneId = telefoneId,
                UsuarioId = usuarioId,
                Acao = "Vincular",
                DataHora = DateTime.UtcNow
            };
            _context.ColaboradorTelefoneLog.Add(log);

            await _context.SaveChangesAsync();
            return vinculo;
        }

        public async Task<bool> DesvincularAsync(int colaboradorId, int telefoneId, int usuarioId)
        {
            var vinculo = await _context.ColaboradoresTelefones
                .FirstOrDefaultAsync(ct => ct.ColaboradorId == colaboradorId && ct.TelefoneId == telefoneId);

            if (vinculo == null) return false;

            _context.ColaboradoresTelefones.Remove(vinculo);

            var log = new ColaboradorTelefoneLog
            {
                ColaboradorId = colaboradorId,
                TelefoneId = telefoneId,
                UsuarioId = usuarioId,
                Acao = "Desvincular",
                DataHora = DateTime.UtcNow
            };
            _context.ColaboradorTelefoneLog.Add(log);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ColaboradorTelefone>> ObterPorColaboradorAsync(int colaboradorId)
        {
            return await _context.ColaboradoresTelefones
                .Include(ct => ct.Telefone)
                .Where(ct => ct.ColaboradorId == colaboradorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ColaboradorTelefone>> ObterPorTelefoneAsync(int telefoneId)
        {
            return await _context.ColaboradoresTelefones
                .Include(ct => ct.Colaborador)
                .Where(ct => ct.TelefoneId == telefoneId)
                .ToListAsync();
        }
    }
}
