using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [StringLength(50)]
        public string Tipo { get; set; }

        [StringLength(50)]
        public string Patrimonio { get; set; }

        [StringLength(20)]
        public string Status { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? AtualizadoEm { get; set; }

        public int CriadoPor { get; set; }
        public int? AtualizadoPor { get; set; }

        public ICollection<ColaboradorTelefone> ColaboradoresTelefones { get; set; }
    }
}
