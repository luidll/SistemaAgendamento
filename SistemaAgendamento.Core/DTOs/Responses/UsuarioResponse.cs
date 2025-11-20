namespace SistemaAgendamento.Application.DTOs.Responses
{
    public class UsuarioResponse
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = "";
        public string Login { get; set; } = "";
        public string Email { get; set; } = "";
        public string Cpf { get; set; } = string.Empty;
        public string? Telefone { get; set; }
        public string? Setor { get; set; }
        public string? Ramal { get; set; }
        public List<RotinaResponse> Rotinas { get; set; } = new();
    }
}
