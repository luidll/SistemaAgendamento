using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> ListarAsync();
        Task<Usuario?> ObterPorIdAsync(int id);
        Task CriarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task ExcluirAsync(Usuario usuario);
        Task<bool> LoginExiste(string login, int? ignorarId = null);
        Task<Usuario?> ObterComRotinasAsync(int id);
    }
}
