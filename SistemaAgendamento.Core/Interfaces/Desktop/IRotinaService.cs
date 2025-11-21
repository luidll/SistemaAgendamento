using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces.Desktop
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
