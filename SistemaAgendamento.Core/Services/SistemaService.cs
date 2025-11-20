using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Services
{
    public class SistemaService : ISistemaService
    {
        private readonly ISistemaRepository _repository;
        private readonly IMapper _mapper;

        public SistemaService(ISistemaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<SistemaResponse>> GetAllAsync()
        {
            var sistemas = await _repository.GetAllAsync();
            return _mapper.Map<List<SistemaResponse>>(sistemas);
        }

        public async Task<SistemaResponse?> GetByIdAsync(int id)
        {
            var sistema = await _repository.GetByIdAsync(id);
            return _mapper.Map<SistemaResponse?>(sistema);
        }

        public async Task<int> AddOrUpdateAsync(SistemaRequest request)
        {
            var sistema = _mapper.Map<Sistema>(request);

            if (sistema.Id == 0)
                await _repository.AddAsync(sistema);
            else
                await _repository.UpdateAsync(sistema);

            return sistema.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var sistema = await _repository.GetByIdAsync(id);
            if (sistema != null)
            {
                await _repository.DeleteAsync(sistema);
            }
        }
    }
}
