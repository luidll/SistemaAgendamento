using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;
using SistemaAgendamento.Application.Interfaces.Web;
using Microsoft.EntityFrameworkCore;

namespace SistemaAgendamento.Infrastructure.Repositories.Web
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AppDbContext _db;

        public AgendamentoRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<Agendamento?> GetByIdAsync(int id)
        {
            return await _db.Agendamentos.FindAsync(id);
        }

        public async Task<List<Agendamento>> GetByUserIdAsync(int userId, DateTime? start, DateTime? end)
        {
            var query = _db.Agendamentos.AsNoTracking()
                .Include(a => a.Sala).Include(a => a.Usuario)
                .Where(a => a.UsuarioId == userId);

            if (start.HasValue && end.HasValue)
            {
                query = query.Where(a => a.DataHoraInicio >= start.Value && a.DataHoraFim <= end.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Agendamento?> GetAgendamentoByIdAsync(int id)
        {
            return await _db.Agendamentos.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Agendamento agendamento)
        {
            _db.Agendamentos.Add(agendamento);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Agendamento agendamento)
        {
            _db.Agendamentos.Update(agendamento);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var agendamento = await _db.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _db.Agendamentos.Remove(agendamento);
                await _db.SaveChangesAsync();
            }
        }
    }
}