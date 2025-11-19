using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Domain.Entities
{
    public class Sala
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da sala é obrigatório")]
        public string Nome { get; set; } = string.Empty;

        public int Capacidade { get; set; }

        public string Localizacao { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public string? ImagemUrl { get; set; }
    }
}