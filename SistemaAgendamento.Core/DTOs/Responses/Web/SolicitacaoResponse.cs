using SistemaAgendamento.Domain.Enums;
using System;

namespace SistemaAgendamento.Application.DTOs.Responses.Web
{
    public class SolicitacaoResponse
    {
        public int Id { get; set; }
        public string SolicitanteNome { get; set; } = string.Empty;
        public string SolicitadoNome { get; set; } = string.Empty;
        public string SalaNome { get; set; } = string.Empty;
        public DateTime DataHoraInicio { get; set; }
        public DateTime DataHoraFim { get; set; }
        public string Justificativa { get; set; } = string.Empty;
        public StatusSolicitacao Status { get; set; }
        public DateTime DataSolicitacao { get; set; }
    }
}