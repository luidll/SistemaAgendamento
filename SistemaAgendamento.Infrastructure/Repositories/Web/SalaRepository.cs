using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;

namespace SistemaAgendamento.Infrastructure.Repositories.Web
{
    public class SalaRepository : ISalaRepository
    {
        private readonly AppDbContext _db;

        public SalaRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Sala>> GetAllAsync()
        {
            return await _db.Salas.ToListAsync();
        }

        public async Task<Sala?> GetByIdAsync(int id)
        {
            return await _db.Salas.FindAsync(id);
        }

        public async Task AddAsync(Sala sala)
        {
            await _db.Salas.AddAsync(sala);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sala sala)
        {
            _db.Salas.Update(sala);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sala = await _db.Salas.FindAsync(id);
            if (sala != null)
            {
                _db.Salas.Remove(sala);
                await _db.SaveChangesAsync();
            }
        }
    }
}
