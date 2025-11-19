using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;

namespace SistemaAgendamento.Infrastructure.Repositories
{
    public class SistemaRepository : ISistemaRepository
    {
        private readonly AppDbContext _context;

        public SistemaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sistema>> GetAllAsync()
        {
            return await _context.Sistemas.AsNoTracking().ToListAsync();
        }

        public async Task<Sistema?> GetByIdAsync(int id)
        {
            return await _context.Sistemas.FindAsync(id);
        }

        public async Task AddAsync(Sistema sistema)
        {
            _context.Sistemas.Add(sistema);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sistema sistema)
        {
            _context.Sistemas.Update(sistema);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sistema = await _context.Sistemas.FindAsync(id);
            if (sistema != null)
            {
                _context.Sistemas.Remove(sistema);
                await _context.SaveChangesAsync();
            }
        }
    }
}
