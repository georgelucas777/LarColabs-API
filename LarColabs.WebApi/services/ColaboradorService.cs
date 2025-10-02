using LarColabs.WebApi.Database;
using LarColabs.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LarColabs.WebApi.Services
{
    public class ColaboradorService
    {
        private readonly LarColabsContext _context;

        public ColaboradorService(LarColabsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Colaborador>> ObterTodosAsync()
        {
            return await _context.Colaboradores
                .Include(c => c.ColaboradoresTelefones)
                .ThenInclude(ct => ct.Telefone)
                .ToListAsync();
        }

        public async Task<Colaborador?> ObterPorIdAsync(int id)
        {
            return await _context.Colaboradores
                .Include(c => c.ColaboradoresTelefones)
                .ThenInclude(ct => ct.Telefone)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Colaborador> AdicionarAsync(Colaborador colaborador, int usuarioId)
        {
            colaborador.CriadoPor = usuarioId;
            colaborador.CriadoEm = DateTime.UtcNow;
            colaborador.Ativo = true;

            _context.Colaboradores.Add(colaborador);
            await _context.SaveChangesAsync();
            return colaborador;
        }

        public async Task<Colaborador?> AtualizarAsync(int id, Colaborador colaboradorAtualizado, int usuarioId)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null) return null;

            colaborador.NomeCompleto = colaboradorAtualizado.NomeCompleto;
            colaborador.CPF = colaboradorAtualizado.CPF;
            colaborador.DataNascimento = colaboradorAtualizado.DataNascimento;
            colaborador.Ativo = colaboradorAtualizado.Ativo;

            colaborador.AtualizadoPor = usuarioId;
            colaborador.AtualizadoEm = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return colaborador;
        }

        public async Task<bool> RemoverAsync(int id)
        {
            var colaborador = await _context.Colaboradores.FindAsync(id);
            if (colaborador == null) return false;

            _context.Colaboradores.Remove(colaborador);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
