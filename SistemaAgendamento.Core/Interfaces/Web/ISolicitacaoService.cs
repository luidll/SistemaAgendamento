using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;

namespace SistemaAgendamento.Application.Interfaces.Web
{
    public interface ISolicitacaoService
    {
        Task CriarSolicitacaoAsync(SolicitacaoRequest request, int usuarioSolicitanteId);
        Task AceitarSolicitacaoAsync(int solicitacaoId, int usuarioLogadoId);
        Task RecusarSolicitacaoAsync(int solicitacaoId, int usuarioLogadoId);
        Task<List<SolicitacaoResponse>> GetRecebidasAsync(int usuarioId);
        Task<List<SolicitacaoResponse>> GetEnviadasAsync(int usuarioId);
    }
}