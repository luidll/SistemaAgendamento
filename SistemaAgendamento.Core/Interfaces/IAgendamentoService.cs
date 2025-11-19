using Entities = SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface IAgendamentoService
    {
        Task<Entities.Agendamento> AddAsync(Entities.Agendamento agendamento);
        Task UpdateAsync(Entities.Agendamento agendamento);
        Task DeleteAsync(int id);
        Task<List<Entities.Agendamento>> GetByUserIdAsync(int userId, DateTime? start = null, DateTime? end = null);

        Task<Entities.Agendamento?> GetByIdAsync(int id);
    }
}