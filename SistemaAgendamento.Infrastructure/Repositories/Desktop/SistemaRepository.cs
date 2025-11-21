using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.Interfaces.Desktop;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;

namespace SistemaAgendamento.Infrastructure.Repositories.Desktop
{
    public class SistemaRepository : ISistemaRepository
    {
        private readonly AppDbContext _db;

        public SistemaRepository(AppDbContext context)
        {
            _db = context;
        }

        public async Task<List<Sistema>> GetAllAsync()
        {
            return await _db.Sistemas.AsNoTracking().ToListAsync();
        }

        public async Task<Sistema?> GetByIdAsync(int id)
        {
            return await _db.Sistemas.FindAsync(id);
        }

        public async Task AddAsync(Sistema sistema)
        {
            _db.Sistemas.Add(sistema);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sistema sistema)
        {
            _db.Sistemas.Update(sistema);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Sistema sistema)
        {
            _db.Sistemas.Remove(sistema);
            await _db.SaveChangesAsync();
        }
    }
}
