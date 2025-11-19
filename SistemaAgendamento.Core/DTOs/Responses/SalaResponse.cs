namespace SistemaAgendamento.Application.DTOs.Responses
{
    public class SalaResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Localizacao { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string? ImagemUrl { get; set; }
    }
}
