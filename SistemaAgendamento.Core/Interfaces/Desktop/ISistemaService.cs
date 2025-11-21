using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.Interfaces.Desktop
{
    public interface ISistemaService
    {
        Task<List<SistemaResponse>> GetAllAsync();
        Task<SistemaResponse?> GetByIdAsync(int id);
        Task<int> AddOrUpdateAsync(SistemaRequest request);
        Task DeleteAsync(int id);
    }
}
