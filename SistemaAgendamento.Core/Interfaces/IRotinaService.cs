using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface IRotinaService
    {
        Task<List<RotinaResponse>> ListarAsync();
        Task<List<RotinaResponse>> ListarAtivasAsync();
        Task<RotinaResponse?> ObterPorIdAsync(int id);
        Task<RotinaResponse> CriarAsync(RotinaRequest dto);
        Task<RotinaResponse?> AtualizarAsync(int id, RotinaRequest dto);
        Task<bool> DeletarAsync(int id);
        Task<List<RotinaResponse>> ObterPorIdsAsync(IEnumerable<int> ids);
        Task<List<Modulo>> GetModulosAtivosAsync();
    }
}
