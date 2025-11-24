using SistemaAgendamento.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.Interfaces.Web
{
    public interface ISolicitacaoRepository
    {
        Task AddAsync(Solicitacao solicitacao);
        Task UpdateAsync(Solicitacao solicitacao);
        Task<Solicitacao?> GetByIdAsync(int id);
        Task<List<Solicitacao>> GetRecebidasAsync(int usuarioId);
        Task<List<Solicitacao>> GetEnviadasAsync(int usuarioId);
        Task<List<Solicitacao>> GetFinalizadasAsync(int usuarioId);
        Task<Solicitacao?> GetByIdWithAgendamentoAsync(int id);
    }
}