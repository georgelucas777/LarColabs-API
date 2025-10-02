namespace LarColabs.WebApi.DTOs
{
    public class TelefoneCreateDto
    {
        public string DDD { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Patrimonio { get; set; }
        public string Status { get; set; }
    }

    public class TelefoneUpdateDto
    {
        public string DDD { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Patrimonio { get; set; }
        public string Status { get; set; }
    }

    public class TelefoneReadDto
    {
        public int Id { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Patrimonio { get; set; }
        public string Status { get; set; }

        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
