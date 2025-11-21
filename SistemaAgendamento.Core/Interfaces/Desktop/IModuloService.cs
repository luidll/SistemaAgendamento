using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;

namespace SistemaAgendamento.Application.Interfaces.Desktop
{
    public interface IModuloService
    {
        Task<List<ModuloResponse>> GetAllAsync();
        Task<ModuloResponse?> GetByIdAsync(int id);
        Task<int> AddOrUpdateAsync(ModuloRequest request);
        Task DeleteAsync(int id);
    }
}
