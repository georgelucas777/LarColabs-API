using Microsoft.EntityFrameworkCore;
using LarColabs.WebApi.Models;

namespace LarColabs.WebApi.Database
{
    public class LarColabsContext : DbContext
    {
        public LarColabsContext(DbContextOptions<LarColabsContext> options) : base(options) { }

        public DbSet<Colaborador> Colaboradores { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<ColaboradorTelefone> ColaboradoresTelefones { get; set; }
        public DbSet<ColaboradorTelefoneLog> ColaboradorTelefoneLog { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioLoginLog> UsuarioLoginLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColaboradorTelefone>()
                .HasIndex(ct => new { ct.ColaboradorId, ct.TelefoneId })
                .IsUnique();

            modelBuilder.Entity<Colaborador>()
                .HasIndex(c => c.CPF)
                .IsUnique();

            modelBuilder.Entity<Telefone>()
                .HasIndex(t => new { t.DDD, t.Numero })
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Cpf)
                .IsUnique();

            modelBuilder.Entity<UsuarioLoginLog>()
                .HasOne(log => log.Usuario)
                .WithMany()
                .HasForeignKey(log => log.UsuarioId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
