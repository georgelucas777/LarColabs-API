using System.Text.Json.Serialization;

namespace LarColabs.WebApi.Models
{
    public class ColaboradorTelefone
    {
        public int Id { get; set; }

        public int ColaboradorId { get; set; }

        [JsonIgnore]
        public Colaborador Colaborador { get; set; }

        public int TelefoneId { get; set; }
        public Telefone Telefone { get; set; }

        public int CriadoPor { get; set; }

        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    }
}
