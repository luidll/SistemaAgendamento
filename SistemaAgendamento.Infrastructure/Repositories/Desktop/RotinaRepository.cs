using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;

namespace SistemaAgendamento.Infrastructure.Repositories.Desktop
{
    public class RotinaRepository : IRotinaRepository
    {
        private readonly AppDbContext _db;

        public RotinaRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Rotina>> GetAllAsync()
        {
            return await _db.Rotinas
                .Include(r => r.Modulo)
                .ThenInclude(m => m.Sistema)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Rotina?> GetByIdAsync(int id)
        {
            return await _db.Rotinas
                .Include(r => r.Modulo)
                .ThenInclude(m => m.Sistema)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddAsync(Rotina rotina)
        {
            _db.Rotinas.Add(rotina);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Rotina rotina)
        {
            _db.Rotinas.Update(rotina);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Rotina rotina)
        {
            _db.Rotinas.Remove(rotina);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Modulo>> GetModulosAtivosAsync()
        {
            return await _db.Modulos
                .Where(m => m.Ativo)
                .Include(m => m.Sistema)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<Rotina>> ListarAtivasAsync()
        {
            return await _db.Rotinas
                .Where(r => r.Ativo)
                .Include(r => r.Modulo)
                .ThenInclude(m => m.Sistema)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Rotina>> ObterPorIdsAsync(IEnumerable<int> ids)
        {
            return await _db.Rotinas
                .Where(r => ids.Contains(r.Id))
                .Include(r => r.Modulo)
                .ThenInclude(m => m.Sistema)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}
