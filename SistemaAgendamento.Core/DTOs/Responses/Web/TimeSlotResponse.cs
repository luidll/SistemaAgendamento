using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.DTOs.Responses.Web
{
    public class TimeSlotResponse
    {
        public DateTime Time { get; set; }
        public bool IsAvailable { get; set; }
        public int? ExistingAgendamentoId { get; set; }
    }
}
