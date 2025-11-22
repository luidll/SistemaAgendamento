using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces.Web
{
    public interface ISalaRepository
    {
        Task<List<Sala>> GetAllAsync();
        Task<List<Sala>> GetAllActiveAsync();
        Task<Sala?> GetByIdAsync(int id);
        Task AddAsync(Sala sala);
        Task UpdateAsync(Sala sala);
        Task DeleteAsync(int id);
    }
}