using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Application.Interfaces.Web;
using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;

namespace SistemaAgendamento.Infrastructure.Services.Web
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _repo;
        private readonly IMapper _mapper;

        public SalaService(ISalaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<SalaResponse>> GetAllAsync()
        {
            var salas = await _repo.GetAllAsync();
            return _mapper.Map<List<SalaResponse>>(salas);
        }

        public async Task<SalaResponse?> GetByIdAsync(int id)
        {
            var sala = await _repo.GetByIdAsync(id);
            return _mapper.Map<SalaResponse>(sala);
        }

        public async Task<int> AddOrUpdateAsync(SalaRequest request)
        {
            var sala = _mapper.Map<Sala>(request);

            if (sala.Id == 0)
            {
                await _repo.AddAsync(sala);
            }
            else
            {
                await _repo.UpdateAsync(sala);
            }
            return sala.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}