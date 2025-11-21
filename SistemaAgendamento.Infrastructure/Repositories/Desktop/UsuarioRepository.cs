using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.Interfaces.Desktop;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;

namespace SistemaAgendamento.Infrastructure.Repositories.Desktop
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _db;
        public UsuarioRepository(AppDbContext db) => _db = db;

        public Task<List<Usuario>> ListarAsync() =>
            _db.Usuarios.AsNoTracking().ToListAsync();

        public Task<Usuario?> ObterPorIdAsync(int id) =>
            _db.Usuarios.FirstOrDefaultAsync(u => u.Id == id);

        public async Task CriarAsync(Usuario usuario)
        {
            _db.Usuarios.Add(usuario);
            await _db.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _db.Usuarios.Update(usuario);
            await _db.SaveChangesAsync();
        }

        public async Task ExcluirAsync(Usuario usuario)
        {
            _db.Usuarios.Remove(usuario);
            await _db.SaveChangesAsync();
        }

        public Task<bool> LoginExiste(string login, int? ignorarId = null)
        {
            return _db.Usuarios
                .AnyAsync(u => u.Login == login &&
                               (ignorarId == null || u.Id != ignorarId));
        }
        public async Task<Usuario?> ObterComRotinasAsync(int id)
        {
            return await _db.Usuarios
                .Include(u => u.RotinasPermitidas)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

    }

}
