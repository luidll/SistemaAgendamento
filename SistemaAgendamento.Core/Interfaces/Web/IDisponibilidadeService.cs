using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces.Web
{
    public interface IDisponibilidadeService
    {
        Task<List<TimeSlotResponse>> GenerateTimeSlots(int targetUserId, DateTime date);
    }
}