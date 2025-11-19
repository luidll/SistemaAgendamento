using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface IDisponibilidadeService
    {
        Task AddOrUpdateRegraAsync(RegraDisponibilidade regra);
        Task DeleteRegraAsync(int id);
        Task<List<RegraDisponibilidade>> GetRegrasByUserIdAsync(int userId);
        Task<List<DateTime>> GenerateTimeSlots(int targetUserId, DateTime date);
    }
}