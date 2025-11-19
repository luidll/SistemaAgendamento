using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.WebApp.Services
{
    public class SalaService : ISalaService
    {
        private readonly AppDbContext _dbContext;

        public SalaService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Sala>> GetAllAsync()
        {
            return await _dbContext.Salas.AsNoTracking().ToListAsync();
        }

        public async Task<Sala?> GetByIdAsync(int id)
        {
            return await _dbContext.Salas.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddOrUpdateAsync(Sala sala)
        {
            if (sala.Id == 0)
            {
                _dbContext.Salas.Add(sala);
            }
            else
            {
                _dbContext.Salas.Update(sala);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sala = await _dbContext.Salas.FindAsync(id);
            if (sala != null)
            {
                _dbContext.Salas.Remove(sala);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}