using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Domain.Enums;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    [Authorize]
    public partial class Solicitacoes : ComponentBase
    {
        [Inject] private ISolicitacaoService SolicitacaoService { get; set; } = null!;
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
        [Inject] private NavigationManager NavManager { get; set; } = null!;

        protected List<SolicitacaoResponse> Recebidas { get; set; } = new();
        protected List<SolicitacaoResponse> Enviadas { get; set; } = new();
        protected List<SolicitacaoResponse> Finalizadas { get; set; } = new();
        protected DateTime dataFinalizacao;

        protected int CurrentUserId;
        protected string? MensagemStatus;

        protected override async Task OnInitializedAsync()
        {
            await CarregarDados();
        }

        private async Task CarregarDados()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                CurrentUserId = userId;

                Recebidas = await SolicitacaoService.GetRecebidasAsync(userId);
                Enviadas = await SolicitacaoService.GetEnviadasAsync(userId);
                Finalizadas = await SolicitacaoService.GetFinalizadasAsync(userId);
            }
        }

        protected async Task Aceitar(int id)
        {
            try
            {
                await SolicitacaoService.AceitarSolicitacaoAsync(id, CurrentUserId);
                MensagemStatus = "Solicitação aceita! O horário foi transferido.";
                await CarregarDados();
            }
            catch (Exception ex)
            {
                MensagemStatus = $"Erro ao aceitar: {ex.Message}";
            }
        }

        protected async Task Recusar(int id)
        {
            try
            {
                await SolicitacaoService.RecusarSolicitacaoAsync(id, CurrentUserId);
                MensagemStatus = "Solicitação recusada.";
                await CarregarDados();
            }
            catch (Exception ex)
            {
                MensagemStatus = $"Erro ao recusar: {ex.Message}";
            }
        }

        protected string GetStatusColor(StatusSolicitacao status)
        {
            return status switch
            {
                StatusSolicitacao.Pendente => "warning",
                StatusSolicitacao.Aprovada => "success",
                StatusSolicitacao.Recusada => "danger",
                StatusSolicitacao.Cancelada => "secondary",
                _ => "light"
            };
        }
    }
}