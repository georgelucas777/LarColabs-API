using Microsoft.EntityFrameworkCore;
using LarColabs.WebApi.Models;
using LarColabs.WebApi.Enums;
using System;

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

            modelBuilder.Entity<ColaboradorTelefoneLog>()
                .HasOne<Telefone>()
                .WithMany()
                .HasForeignKey(l => l.TelefoneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ColaboradorTelefoneLog>()
                .HasOne<Colaborador>()
                .WithMany()
                .HasForeignKey(l => l.ColaboradorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Telefone>()
                .Property(t => t.Tipo)
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => (TipoTelefone)Enum.Parse(typeof(TipoTelefone), Capitalize(v))
                );

            modelBuilder.Entity<Telefone>()
                .Property(t => t.Patrimonio)
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => (PatrimonioTelefone)Enum.Parse(typeof(PatrimonioTelefone), Capitalize(v))
                );

            modelBuilder.Entity<Telefone>()
                .Property(t => t.Status)
                .HasConversion(
                    v => v.ToString().ToLower(),
                    v => (StatusTelefone)Enum.Parse(typeof(StatusTelefone), Capitalize(v))
                );

            base.OnModelCreating(modelBuilder);
        }

        private static string Capitalize(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return char.ToUpper(value[0]) + value.Substring(1);
        }
    }
}
