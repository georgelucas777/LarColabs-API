using LarColabs.WebApi.Enums;
using System.Text.Json.Serialization;

namespace LarColabs.WebApi.DTOs
{
    public class TelefoneCreateDto
    {
        public string DDD { get; set; }
        public string Numero { get; set; }
        public TipoTelefone Tipo { get; set; }
        public PatrimonioTelefone Patrimonio { get; set; }
        public StatusTelefone Status { get; set; }
    }

    public class TelefoneUpdateDto
    {
        public string DDD { get; set; }
        public string Numero { get; set; }
        public TipoTelefone Tipo { get; set; }
        public PatrimonioTelefone Patrimonio { get; set; }
        public StatusTelefone Status { get; set; }
    }

    public class TelefoneReadDto
    {
        public int Id { get; set; }
        public string DDD { get; set; }
        public string Numero { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TipoTelefone Tipo { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PatrimonioTelefone Patrimonio { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusTelefone Status { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
