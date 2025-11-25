using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Infrastructure.Services.Web;
using System.Security.Claims;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    public partial class Dashboard : ComponentBase
    {
        [Inject] private IAgendamentoService _agendamentoService { get; set; } = null!;
        [Inject] protected NavigationManager NavManager { get; set; } = default!;
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
        protected DateTime DataAtual { get; set; } = DateTime.Today;
        protected List<AgendamentoResponse>? Agendamentos;
        protected string UserName { get; set; } = "Usuário";
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userIdString = authState.User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserName = authState.User.FindFirstValue(ClaimTypes.GivenName) ?? authState.User.FindFirstValue(ClaimTypes.Name) ?? "Usuário";
            if (int.TryParse(userIdString, out var userId))
            {
                var start = DataAtual;
                var end = start.AddDays(7);
                Agendamentos = await _agendamentoService.GetByUserIdAsync(userId, start, end);
            }
        }
        protected void NavegarParaNovoAgendamento()
        {
            NavManager.NavigateTo("/agendamento");
        }
        protected void NavegarParaDetalhes()
        {
            NavManager.NavigateTo("/minha-agenda");
        }
    }
}
