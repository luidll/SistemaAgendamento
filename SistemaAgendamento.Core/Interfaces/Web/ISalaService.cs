using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;

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
