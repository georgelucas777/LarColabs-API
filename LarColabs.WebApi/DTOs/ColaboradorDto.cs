namespace LarColabs.WebApi.DTOs
{
    public class ColaboradorCreateDto
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateOnly  DataNascimento { get; set; }
        public bool Ativo { get; set; } = true;
    }

    public class ColaboradorUpdateDto
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateOnly  DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }

    public class ColaboradorReadDto
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateOnly  DataNascimento { get; set; }
        public bool Ativo { get; set; }
    }
}
