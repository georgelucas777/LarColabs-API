using System;
using System.ComponentModel.DataAnnotations;

namespace LarColabs.WebApi.Models
{
    public class ColaboradorTelefoneLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ColaboradorId { get; set; }

        [Required]
        public int TelefoneId { get; set; }

        [Required, StringLength(20)]
        public string Acao { get; set; } = string.Empty;

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public DateTime DataHora { get; set; } = DateTime.UtcNow;
    }
}
