using System.ComponentModel.DataAnnotations;

namespace Agendamento.Core.Entities
{
    public class RegraDisponibilidade
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;

        [Required]
        public DayOfWeek DiaDaSemana { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFim { get; set; }

        [Required]
        public int DuracaoSlotMinutos { get; set; }
    }
}