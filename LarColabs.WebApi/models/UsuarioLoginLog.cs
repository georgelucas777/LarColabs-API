using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LarColabs.WebApi.Models
{
    public class UsuarioLoginLog
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime DataHora { get; set; } = DateTime.UtcNow;
    }
}
