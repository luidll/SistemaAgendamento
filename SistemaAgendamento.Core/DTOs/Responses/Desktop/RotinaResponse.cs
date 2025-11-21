namespace SistemaAgendamento.Application.DTOs.Responses.Desktop
{
    public class RotinaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public int ModuloId { get; set; }
        public string ModuloNome { get; set; } = string.Empty;
        public string SistemaNome { get; set; } = string.Empty;
    }
}
