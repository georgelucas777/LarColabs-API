using System;
using System.ComponentModel.DataAnnotations;

namespace LarColabs.WebApi.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Nome { get; set; }

        [Required, MaxLength(150)]
        public string Email { get; set; }

        [Required, StringLength(11)]
        public string Cpf { get; set; }

        [Required]
        public string Senha { get; set; }

        public bool Ativo { get; set; } = true;

        [Required]
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;

        public DateTime? AtualizadoEm { get; set; }
    }
}
