using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface IRotinaService
    {
        Task<List<RotinaResponse>> GetAllAsync();
        Task<List<RotinaResponse>> ListarAtivasAsync();
        Task<RotinaResponse?> GetByIdAsync(int id);
        Task<int> AddOrUpdateAsync(RotinaRequest request);
        Task<bool> DeletarAsync(int id);
        Task<List<RotinaResponse>> ObterPorIdsAsync(IEnumerable<int> ids);
        Task<List<Modulo>> GetModulosAtivosAsync();
    }
}
