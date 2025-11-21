using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAgendamento.Application.Interfaces.Desktop
{
    public interface IUsuarioService
    {
        Task<List<UsuarioResponse>> ListarAsync();
        Task<UsuarioRequest?> ObterParaEdicaoAsync(int id);
        Task<string?> SalvarAsync(UsuarioRequest request);
        Task ExcluirAsync(int id);
        Task<UsuarioResponse?> ObterComPermissoesAsync(int id);
        Task ConcederRotinasAsync(int usuarioId, int[] rotinaIds);
        Task RevogarRotinasAsync(int usuarioId, int[] rotinaIds);
    }

}
