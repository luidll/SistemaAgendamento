using System;
using System.ComponentModel.DataAnnotations;
using SistemaAgendamento.Domain.Enums;

namespace SistemaAgendamento.Domain.Entities
{
    public class Solicitacao
    {
        public int Id { get; set; }

        public int SolicitanteId { get; set; }
        public virtual Usuario Solicitante { get; set; } = null!;

        public int SolicitadoId { get; set; }
        public virtual Usuario Solicitado { get; set; } = null!;

        public int AgendamentoId { get; set; }
        public virtual Agendamento Agendamento { get; set; } = null!;

        [Required]
        public string Justificativa { get; set; } = string.Empty;

        public DateTime DataSolicitacao { get; set; } = DateTime.Now;

        public StatusSolicitacao Status { get; set; } = StatusSolicitacao.Pendente;

        public string? RespostaObservacao { get; set; }

        [Required]
        public DateTime DataHoraInicioSolicitada { get; set; }

        [Required]
        public DateTime DataHoraFimSolicitada { get; set; }
        public bool Finalizado { get; set; }
    }
}