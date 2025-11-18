using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Agendamento.WebApp.Services
{
    public class DisponibilidadeService : IDisponibilidadeService
    {
        private readonly AppDbContext _dbContext;
        private readonly IAgendamentoService _agendamentoService;

        public DisponibilidadeService(AppDbContext dbContext, IAgendamentoService agendamentoService)
        {
            _dbContext = dbContext;
            _agendamentoService = agendamentoService;
        }

        public async Task<List<DateTime>> GenerateTimeSlots(int targetUserId, DateTime date)
        {
            var availableSlots = new List<DateTime>();
            var dayOfWeek = date.DayOfWeek;

            var regras = await _dbContext.RegrasDisponibilidade
                .Where(r => r.UsuarioId == targetUserId && r.DiaDaSemana == dayOfWeek)
                .AsNoTracking()
                .ToListAsync();

            if (!regras.Any()) return availableSlots;

            var compromissos = await _agendamentoService.GetByUserIdAsync(targetUserId, date.Date, date.Date.AddDays(1));

            // 3. Loop de Cálculo (Estrutura Arquitetural)
            foreach (var regra in regras)
            {
                var slotDuration = TimeSpan.FromMinutes(regra.DuracaoSlotMinutos);
                var currentTime = date.Date.Add(regra.HoraInicio);
                var endTime = date.Date.Add(regra.HoraFim);

                while (currentTime.Add(slotDuration) <= endTime)
                {
                    bool isConflict = compromissos.Any(a =>
                        a.DataHoraInicio < currentTime.Add(slotDuration) &&
                        a.DataHoraFim > currentTime
                    );

                    if (!isConflict)
                    {
                        availableSlots.Add(currentTime);
                    }

                    currentTime = currentTime.Add(slotDuration);
                }
            }

            return availableSlots;
        }
        public async Task AddOrUpdateRegraAsync(RegraDisponibilidade regra)
        {
            if (regra.Id == 0)
            {
                _dbContext.RegrasDisponibilidade.Add(regra);
            }
            else
            {
                _dbContext.RegrasDisponibilidade.Update(regra);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRegraAsync(int id)
        {
            var regra = await _dbContext.RegrasDisponibilidade.FindAsync(id);
            if (regra != null)
            {
                _dbContext.RegrasDisponibilidade.Remove(regra);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<RegraDisponibilidade>> GetRegrasByUserIdAsync(int userId)
        {
            return await _dbContext.RegrasDisponibilidade
                .Where(r => r.UsuarioId == userId)
                .OrderBy(r => r.DiaDaSemana)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}