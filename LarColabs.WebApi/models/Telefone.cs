using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LarColabs.WebApi.Enums;

namespace LarColabs.WebApi.Models
{
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(3)]
        public string DDD { get; set; }

        [Required, StringLength(15)]
        public string Numero { get; set; }

        [Required]
        public TipoTelefone Tipo { get; set; }

        [Required]
        public PatrimonioTelefone Patrimonio { get; set; }

        [Required]
        public StatusTelefone Status { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? AtualizadoEm { get; set; }

        public int CriadoPor { get; set; }
        public int? AtualizadoPor { get; set; }

        public ICollection<ColaboradorTelefone> ColaboradoresTelefones { get; set; } 
            = new List<ColaboradorTelefone>();
    }
}
