using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Application.Interfaces.Repositories;
using SistemaAgendamento.Domain.Entities;

namespace SistemaAgendamento.Application.Services
{
    public class ModuloService : IModuloService
    {
        private readonly IModuloRepository _repo;
        private readonly IMapper _mapper;

        public ModuloService(IModuloRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<ModuloResponse>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return _mapper.Map<List<ModuloResponse>>(list);
        }

        public async Task<ModuloResponse?> GetByIdAsync(int id)
        {
            var modulo = await _repo.GetByIdAsync(id);
            return _mapper.Map<ModuloResponse>(modulo);
        }

        public async Task<int> AddOrUpdateAsync(ModuloRequest request)
        {
            var modulo = _mapper.Map<Modulo>(request);

            if (modulo.Id == 0)
                await _repo.AddAsync(modulo);
            else
                await _repo.UpdateAsync(modulo);

            await _repo.SaveChangesAsync();
            return modulo.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var modulo = await _repo.GetByIdAsync(id);
            if (modulo != null)
            {
                await _repo.DeleteAsync(modulo);
                await _repo.SaveChangesAsync();
            }
        }
    }
}
