using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.DTOs.Requests.Web
{
    public class AgendamentoRequest
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título do agendamento é obrigatório.")]
        public string Titulo { get; set; } = string.Empty;

        public string? Descricao { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataHoraInicio { get; set; }

        public DateTime DataHoraFim { get; set; }

        public int SalaId { get; set; }
        public int UsuarioId { get; set; }
    }
}
