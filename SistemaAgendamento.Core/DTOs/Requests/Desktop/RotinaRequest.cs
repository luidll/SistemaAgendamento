namespace SistemaAgendamento.Application.DTOs.Requests.Desktop
{
    public class RotinaRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public bool Ativo { get; set; }
        public int ModuloId { get; set; }
    }
}
