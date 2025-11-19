using SistemaAgendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Entities = SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Application.Interfaces;

namespace SistemaAgendamento.Infrastructure.Services
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        public AgendamentoService(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Entities.Agendamento>> GetByUserIdAsync(int userId, DateTime? start, DateTime? end)
        {
            using var context = _factory.CreateDbContext();

            return await context.Agendamentos
                .Where(a => a.UsuarioId == userId)
                .ToListAsync();
        }

        public async Task<Entities.Agendamento?> GetByIdAsync(int id)
        {
            using var context = _factory.CreateDbContext();

            return await context.Agendamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Entities.Agendamento> AddAsync(Entities.Agendamento agendamento)
        {
            using var context = _factory.CreateDbContext();

            context.Agendamentos.Add(agendamento);
            await context.SaveChangesAsync();

            return agendamento;
        }

        public async Task UpdateAsync(Entities.Agendamento agendamento)
        {
            using var context = _factory.CreateDbContext();

            context.Agendamentos.Update(agendamento);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = _factory.CreateDbContext();

            var agendamento = await context.Agendamentos.FindAsync(id);

            if (agendamento != null)
            {
                context.Agendamentos.Remove(agendamento);
                await context.SaveChangesAsync();
            }
        }
    }
}
