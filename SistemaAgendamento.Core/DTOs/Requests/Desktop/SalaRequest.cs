using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Application.DTOs.Requests.Desktop
{
    public class SalaRequest
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;
        [Range(1, 500, ErrorMessage = "A capacidade deve ser maior que zero.")]
        public int Capacidade { get; set; }
        [Required(ErrorMessage = "A localização é obrigatória.")]
        public string Localizacao { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string? ImagemUrl { get; set; }
        public bool Ativo { get; set; } = true;

    }

}
