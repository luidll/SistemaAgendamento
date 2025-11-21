using System.ComponentModel.DataAnnotations;

namespace SistemaAgendamento.Application.DTOs.Requests.Web
{
    public class SolicitacaoRequest
    {
        [Required]
        public int AgendamentoId { get; set; }

        [Required(ErrorMessage = "A justificativa é obrigatória.")]
        public string Justificativa { get; set; } = string.Empty;
    }
}