using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Application.DTOs.Requests
{
    public class UsuarioRequest
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = "";
        public string Login { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Telefone { get; set; }
        public string? Setor { get; set; }
        public string? Ramal { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string? Senha { get; set; }
        public string? ConfirmarSenha { get; set; }
    }

}
