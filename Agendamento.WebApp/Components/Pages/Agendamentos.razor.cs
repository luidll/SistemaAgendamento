using Entities = Agendamento.Core.Entities;
using Agendamento.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Agendamento.WebApp.Components.Pages
{
    [Authorize]
    public partial class Agendamentos : ComponentBase
    {
        [Inject]
        private IAgendamentoService AgendamentoService { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        private Entities.Agendamento agendamentoAtual { get; set; } = new Entities.Agendamento();
        private string? mensagemErro;
        private int _currentUserId;

        private List<Entities.Agendamento> listaAgendamentos = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity?.IsAuthenticated == true)
            {
                var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdString, out var userId))
                {
                    _currentUserId = userId;
                    await LoadAgendamentos();
                }
            }
        }

        private async Task LoadAgendamentos()
        {
            listaAgendamentos = await AgendamentoService.GetByUserIdAsync(_currentUserId);
        }

        protected async Task SalvarAgendamento()
        {
            mensagemErro = null;

            if (_currentUserId == 0)
            {
                mensagemErro = "Usuário não autenticado. Redirecionando...";
                NavManager.NavigateTo("/", true);
                return;
            }

            agendamentoAtual.UsuarioId = _currentUserId;

            try
            {
                if (agendamentoAtual.Id == 0)
                {
                    await AgendamentoService.AddAsync(agendamentoAtual);
                }
                else
                {
                    await AgendamentoService.UpdateAsync(agendamentoAtual);
                }

                LimparFormulario();
                await LoadAgendamentos();
            }
            catch (Exception ex)
            {
                mensagemErro = $"Erro ao salvar: {ex.Message}";
            }
        }

        protected void LimparFormulario()
        {
            agendamentoAtual = new Entities.Agendamento();
            agendamentoAtual.UsuarioId = _currentUserId;
        }

        protected async Task CarregarParaEdicao(int agendamentoId)
        {
            var ag = await AgendamentoService.GetByIdAsync(agendamentoId);
            if (ag != null)
            {
                agendamentoAtual = ag;
            }
        }

        protected async Task DeletarAgendamento(int agendamentoId)
        {
            await AgendamentoService.DeleteAsync(agendamentoId);
            await LoadAgendamentos();
        }
    }
}