using SistemaAgendamento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.Interfaces
{
    public interface ISistemaRepository
    {
        Task<List<Sistema>> GetAllAsync();
        Task<Sistema?> GetByIdAsync(int id);
        Task AddAsync(Sistema sistema);
        Task UpdateAsync(Sistema sistema);
        Task DeleteAsync(int id);
    }
}
