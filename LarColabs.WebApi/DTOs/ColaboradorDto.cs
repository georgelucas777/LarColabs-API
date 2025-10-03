namespace LarColabs.WebApi.DTOs
{
    public class ColaboradorCreateDto
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateOnly DataNascimento { get; set; }
        public bool Ativo { get; set; } = true;
    }

    public class ColaboradorUpdateDto
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateOnly DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }

    public class ColaboradorReadDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateOnly DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }

    public class TelefoneDto
    {
        public int Id { get; set; }
        public string DDD { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string Patrimonio { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }

    public class ColaboradorTelefonesDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public IEnumerable<TelefoneDto> Telefones { get; set; } = new List<TelefoneDto>();
    }
}
