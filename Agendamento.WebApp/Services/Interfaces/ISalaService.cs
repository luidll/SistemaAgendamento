using Agendamento.Core.Entities;

namespace Agendamento.WebApp.Services
{
    public interface ISalaService
    {
        Task<List<Sala>> GetAllAsync();
        Task<Sala?> GetByIdAsync(int id);
        Task AddOrUpdateAsync(Sala sala);
        Task DeleteAsync(int id);
    }
}