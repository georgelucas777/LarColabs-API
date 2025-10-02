using System.Runtime.Serialization;

namespace LarColabs.WebApi.Enums
{
    public enum TipoTelefone
    {
        [EnumMember(Value = "movel")]
        Movel = 1,

        [EnumMember(Value = "fixo")]
        Fixo = 2
    }

    public enum PatrimonioTelefone
    {
        [EnumMember(Value = "pessoal")]
        Pessoal = 1,

        [EnumMember(Value = "corporativo")]
        Corporativo = 2
    }

    public enum StatusTelefone
    {
        [EnumMember(Value = "ativo")]
        Ativo = 1,

        [EnumMember(Value = "desativado")]
        Desativado = 2,

        [EnumMember(Value = "manutencao")]
        Manutencao = 3
    }
}
