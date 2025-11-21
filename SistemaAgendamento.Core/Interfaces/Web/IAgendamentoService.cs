using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces.Web
{
    public interface IAgendamentoService
    {
        Task<int> AddOrUpdateAsync(AgendamentoRequest request);
        Task DeleteAsync(int id);

        Task<List<AgendamentoResponse>> GetByUserIdAsync(int userId, DateTime? start = null, DateTime? end = null);
        Task<AgendamentoResponse?> GetByIdAsync(int id);
        Task<Agendamento?> GetAgendamentoByIdAsync(int id);
    }
}