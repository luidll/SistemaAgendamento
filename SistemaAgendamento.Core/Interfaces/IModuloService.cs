using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface IModuloService
    {
        Task<List<ModuloResponse>> GetAllAsync();
        Task<ModuloResponse?> GetByIdAsync(int id);
        Task<int> AddOrUpdateAsync(ModuloRequest request);
        Task DeleteAsync(int id);
    }
}
