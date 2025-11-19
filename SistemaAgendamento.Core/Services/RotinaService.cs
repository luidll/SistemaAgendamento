using AutoMapper;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.Services
{
    public class RotinaService : IRotinaService
    {
        private readonly IRotinaRepository _repo;
        private readonly IMapper _mapper;

        public RotinaService(IRotinaRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<RotinaResponse>> ListarAsync()
        {
            var rotinas = await _repo.GetAllAsync();
            return _mapper.Map<List<RotinaResponse>>(rotinas);
        }

        public async Task<List<RotinaResponse>> ListarAtivasAsync()
        {
            var rotinas = await _repo.ListarAtivasAsync();
            return _mapper.Map<List<RotinaResponse>>(rotinas);
        }

        public async Task<RotinaResponse?> ObterPorIdAsync(int id)
        {
            var rotina = await _repo.GetByIdAsync(id);
            return rotina == null ? null : _mapper.Map<RotinaResponse>(rotina);
        }

        public async Task<RotinaResponse> CriarAsync(RotinaRequest dto)
        {
            var rotina = _mapper.Map<Rotina>(dto);
            await _repo.AddAsync(rotina);
            return _mapper.Map<RotinaResponse>(rotina);
        }

        public async Task<RotinaResponse?> AtualizarAsync(int id, RotinaRequest dto)
        {
            var rotina = await _repo.GetByIdAsync(id);
            if (rotina == null) return null;

            _mapper.Map(dto, rotina);
            await _repo.UpdateAsync(rotina);
            return _mapper.Map<RotinaResponse>(rotina);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var rotina = await _repo.GetByIdAsync(id);
            if (rotina == null) return false;

            await _repo.DeleteAsync(rotina);
            return true;
        }

        public async Task<List<RotinaResponse>> ObterPorIdsAsync(IEnumerable<int> ids)
        {
            var rotinas = await _repo.ObterPorIdsAsync(ids);
            return _mapper.Map<List<RotinaResponse>>(rotinas);
        }

        public async Task<List<Modulo>> GetModulosAtivosAsync()
        {
            return await _repo.GetModulosAtivosAsync();
        }
    }
}
