using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface ISalaService
    {
        Task<List<SalaResponse>> GetAllAsync();
        Task<SalaResponse?> GetByIdAsync(int id);
        Task<int> AddOrUpdateAsync(SalaRequest sala);
        Task DeleteAsync(int id);
    }
}
