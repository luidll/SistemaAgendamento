using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface IRotinaRepository
    {
        Task<List<Rotina>> GetAllAsync();
        Task<Rotina?> GetByIdAsync(int id);
        Task AddAsync(Rotina rotina);
        Task UpdateAsync(Rotina rotina);
        Task DeleteAsync(Rotina rotina);
        Task<List<Modulo>> GetModulosAtivosAsync();
        Task<List<Rotina>> ListarAtivasAsync();
        Task<List<Rotina>> ObterPorIdsAsync(IEnumerable<int> ids);

    }
}
