namespace SistemaAgendamento.Application.DTOs.Requests.Desktop
{
    public class ModuloRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public int SistemaId { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
