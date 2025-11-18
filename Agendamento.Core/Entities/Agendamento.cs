using System.ComponentModel.DataAnnotations;

namespace Agendamento.Core.Entities
{
    public class Agendamento
    {
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public string? Descricao { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
    }
}