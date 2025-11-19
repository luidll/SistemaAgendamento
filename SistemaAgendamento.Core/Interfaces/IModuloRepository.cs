using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces.Repositories
{
    public interface IModuloRepository
    {
        Task<List<Modulo>> GetAllAsync();
        Task<Modulo?> GetByIdAsync(int id);
        Task AddAsync(Modulo modulo);
        Task UpdateAsync(Modulo modulo);
        Task DeleteAsync(Modulo modulo);
        Task SaveChangesAsync();
    }
}
