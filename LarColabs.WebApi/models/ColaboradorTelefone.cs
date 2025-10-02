namespace LarColabs.WebApi.Models
{
    public class ColaboradorTelefone
    {
        public int Id { get; set; }

        public int ColaboradorId { get; set; }
        public Colaborador Colaborador { get; set; }

        public int TelefoneId { get; set; }
        public Telefone Telefone { get; set; }

        public string CriadoPor { get; set; }
        public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
        public string AtualizadoPor { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
