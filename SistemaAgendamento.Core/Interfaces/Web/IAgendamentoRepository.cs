using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces.Web
{
    public interface IAgendamentoRepository
    {
        Task AddAsync(Agendamento request);
        Task UpdateAsync(Agendamento request);
        Task<List<Agendamento>> GetByUserIdAsync(int userId, DateTime? start = null, DateTime? end = null);
        Task<Agendamento?> GetByIdAsync(int id);
        Task<Agendamento?> GetAgendamentoByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}