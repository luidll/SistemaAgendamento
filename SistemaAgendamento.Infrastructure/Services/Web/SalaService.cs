using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Application.Interfaces.Web;
using AutoMapper;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Application.DTOs.Requests.Web;

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
        public async Task<List<SalaResponse>> GetAllActiveAsync()
        {
            var salas = await _repo.GetAllActiveAsync();
            return _mapper.Map<List<SalaResponse>>(salas);
        }

        public async Task<SalaResponse?> GetByIdAsync(int id)
        {
            var sala = await _repo.GetByIdAsync(id);
            return _mapper.Map<SalaResponse>(sala);
        }

        public async Task<int> AddOrUpdateAsync(SalaRequest request)
        {
            if (request.Id == 0)
            {
                var novasala = _mapper.Map<Sala>(request);
                await _repo.AddAsync(novasala);
                return novasala.Id;
            }
            var existente = await _repo.GetByIdAsync(request.Id);
            if (existente == null)
                throw new Exception("Sala não encontrada.");

            _mapper.Map(request, existente);
            await _repo.UpdateAsync(existente);

            return existente.Id;
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }
    }
}