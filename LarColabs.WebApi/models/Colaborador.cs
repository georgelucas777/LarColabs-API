using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LarColabs.WebApi.Models
{
    public class Colaborador
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string NomeCompleto { get; set; }

        [Required, StringLength(11)]
        public string CPF { get; set; }

        [Column(TypeName = "date")]
        public DateOnly DataNascimento { get; set; }

        public bool Ativo { get; set; } = true;

        public int CriadoPor { get; set; }
        public int? AtualizadoPor { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public DateTime? AtualizadoEm { get; set; }

        public ICollection<ColaboradorTelefone> ColaboradoresTelefones { get; set; }
            = new List<ColaboradorTelefone>();
    }
}
