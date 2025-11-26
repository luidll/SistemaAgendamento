using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Domain.Entities
{
    public class Agendamento
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public string? Descricao { get; set; }

        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; } = null!;
        public int SalaId { get; set; }
        public virtual Sala Sala { get; set; } = null!;
        public bool Excluído { get; set; }
    }
}