using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface ISistemaService
    {
        Task<List<SistemaResponse>> GetAllAsync();
        Task<SistemaResponse?> GetByIdAsync(int id);
        Task<int> AddOrUpdateAsync(SistemaRequest request);
        Task DeleteAsync(int id);
    }
}
