using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;

namespace SistemaAgendamento.Application.Interfaces.Web
{
    public interface ISalaService
    {
        Task<List<SalaResponse>> GetAllAsync();
        Task<List<SalaResponse>> GetAllActiveAsync();
        Task<SalaResponse?> GetByIdAsync(int id);
        Task<int> AddOrUpdateAsync(SalaRequest sala);
        Task DeleteAsync(int id);
    }
}
