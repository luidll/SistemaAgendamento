using SistemaAgendamento.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;

namespace SistemaAgendamento.Infrastructure.Services.Web
{
    public class DisponibilidadeService : IDisponibilidadeService
    {
        private readonly AppDbContext _dbContext;

        public DisponibilidadeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TimeSlotResponse>> GenerateTimeSlots(int salaId, DateTime date)
        {
            var availableSlots = new List<TimeSlotResponse>();

            var startHour = new TimeSpan(7, 30, 0); // 7:30
            var endHour = new TimeSpan(17, 30, 0); // 17:30
            var slotDuration = TimeSpan.FromMinutes(30);

            var compromissos = await _dbContext.Agendamentos
                .Where(a => a.SalaId == salaId &&
                            a.DataHoraInicio.Date == date.Date && !a.Excluído)
                .AsNoTracking()
                .ToListAsync();

            var currentTime = date.Date.Add(startHour);
            var endTimeLimit = date.Date.Add(endHour);

            while (currentTime.Add(slotDuration) <= endTimeLimit)
            {
                var conflito = compromissos.FirstOrDefault(a =>
            a.DataHoraInicio < currentTime.Add(slotDuration) &&
            a.DataHoraFim > currentTime
        );

                var slot = new TimeSlotResponse
                {
                    Time = currentTime,
                    IsAvailable = (conflito == null)
                };

                if (conflito != null)
                {
                    slot.ExistingAgendamentoId = conflito.Id;
                }

                if (currentTime > DateTime.Now)
                {
                    availableSlots.Add(slot);
                }

                currentTime = currentTime.Add(slotDuration);
            }

            return availableSlots;
        }
    }
}