using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Infrastructure.Services.Web
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _repo;
        private readonly IMapper _mapper;

        public AgendamentoService(IAgendamentoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<AgendamentoResponse>> GetByUserIdAsync(int userId, DateTime? start, DateTime? end)
        {
            var agendamentos = await _repo.GetByUserIdAsync(userId, start, end);

            return _mapper.Map<List<AgendamentoResponse>>(agendamentos);
        }

        public async Task<AgendamentoResponse?> GetByIdAsync(int id)
        {
            var agendamento = await _repo.GetAgendamentoByIdAsync(id);
            return _mapper.Map<AgendamentoResponse>(agendamento);
        }

        public async Task<Agendamento?> GetAgendamentoByIdAsync(int id)
        {
            return await _repo.GetAgendamentoByIdAsync(id);
        }

        public async Task<int> AddOrUpdateAsync(AgendamentoRequest request)
        {
            var agendamento = _mapper.Map<Agendamento>(request);

            if (agendamento.Id == 0)
                await _repo.AddAsync(agendamento);
            else
                await _repo.UpdateAsync(agendamento);

            return agendamento.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}