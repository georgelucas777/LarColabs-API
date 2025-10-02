using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LarColabs.WebApi.Models
{
    public class ColaboradorTelefoneLog
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Colaborador")]
        public int ColaboradorId { get; set; }

        [ForeignKey("Telefone")]
        public int TelefoneId { get; set; }

        [Required, StringLength(20)]
        public string Acao { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime DataHora { get; set; } = DateTime.UtcNow;

        public Colaborador Colaborador { get; set; }
        public Telefone Telefone { get; set; }
    }
}
