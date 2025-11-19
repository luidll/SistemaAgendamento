using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;

namespace SistemaAgendamento.Infrastructure.Services
{
    public class SalaService : ISalaService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public SalaService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<SalaResponse>> GetAllAsync()
        {
            var salas = await _dbContext.Salas.AsNoTracking().ToListAsync();
            return _mapper.Map<List<SalaResponse>>(salas);
        }

        public async Task<SalaResponse?> GetByIdAsync(int id)
        {
            var sala = await _dbContext.Salas.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
            return _mapper.Map<SalaResponse?>(sala);
        }

        public async Task<int> AddOrUpdateAsync(SalaRequest salaDto)
        {
            var sala = _mapper.Map<Sala>(salaDto);

            if (sala.Id == 0)
            {
                _dbContext.Salas.Add(sala);
            }
            else
            {
                _dbContext.Salas.Update(sala);
            }

            await _dbContext.SaveChangesAsync();

            return sala.Id;
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
