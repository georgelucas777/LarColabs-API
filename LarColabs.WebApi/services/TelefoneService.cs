using LarColabs.WebApi.Database;
using LarColabs.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LarColabs.WebApi.Services
{
    public class TelefoneService
    {
        private readonly LarColabsContext _context;

        public TelefoneService(LarColabsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Telefone>> ObterTodosAsync()
        {
            return await _context.Telefones
                                 .Include(t => t.ColaboradoresTelefones)
                                 .ThenInclude(ct => ct.Colaborador)
                                 .ToListAsync();
        }

        public async Task<Telefone?> ObterPorIdAsync(int id)
        {
            return await _context.Telefones
                                 .Include(t => t.ColaboradoresTelefones)
                                 .ThenInclude(ct => ct.Colaborador)
                                 .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Telefone> AdicionarAsync(Telefone telefone, int usuarioId)
        {
            telefone.CriadoPor = usuarioId;
            telefone.CriadoEm = DateTime.UtcNow;

            _context.Telefones.Add(telefone);
            await _context.SaveChangesAsync();
            return telefone;
        }

        public async Task<Telefone?> AtualizarAsync(int id, Telefone telefoneAtualizado, int usuarioId)
        {
            var telefone = await _context.Telefones.FindAsync(id);
            if (telefone == null) return null;

            telefone.DDD = telefoneAtualizado.DDD;
            telefone.Numero = telefoneAtualizado.Numero;
            telefone.Tipo = telefoneAtualizado.Tipo;
            telefone.Patrimonio = telefoneAtualizado.Patrimonio;
            telefone.Status = telefoneAtualizado.Status;

            telefone.AtualizadoPor = usuarioId;
            telefone.AtualizadoEm = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return telefone;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var telefone = await _context.Telefones.FindAsync(id);
            if (telefone == null) return false;

            _context.Telefones.Remove(telefone);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
