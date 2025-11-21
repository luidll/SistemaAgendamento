using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.Interfaces.Desktop;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;

namespace SistemaAgendamento.Infrastructure.Repositories.Desktop
{
    public class ModuloRepository : IModuloRepository
    {
        private readonly AppDbContext _db;

        public ModuloRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Modulo>> GetAllAsync()
        {
            return await _db.Modulos
                .Include(m => m.Sistema)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Modulo?> GetByIdAsync(int id)
        {
            return await _db.Modulos.FindAsync(id);
        }

        public async Task AddAsync(Modulo modulo)
        {
            await _db.Modulos.AddAsync(modulo);
        }

        public Task UpdateAsync(Modulo modulo)
        {
            _db.Modulos.Update(modulo);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Modulo modulo)
        {
            _db.Modulos.Remove(modulo);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
