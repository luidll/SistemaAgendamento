using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Application.DTOs.Responses;
using System.Security.Claims;
using System.Globalization;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAgendamento.Application.DTOs.Responses.Web;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    [Authorize]
    public partial class MinhaAgenda : ComponentBase
    {
        [Inject]
        private IAgendamentoService AgendamentoService { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        protected List<AgendamentoResponse> MeusAgendamentos { get; set; } = new();
        protected string UserName { get; set; } = "Meu";

        protected DateTime DataFiltro { get; set; } = DateTime.Today;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            UserName = user.FindFirstValue(ClaimTypes.GivenName) ?? user.FindFirstValue(ClaimTypes.Name) ?? "Usuário";

            if (user.Identity?.IsAuthenticated == true)
            {
                var userIdString = user.FindFirstValue(ClaimTypes.NameIdentifier);
                if (int.TryParse(userIdString, out var userId))
                {
                    DateTime start = new DateTime(DataFiltro.Year, DataFiltro.Month, 1);
                    DateTime end = start.AddMonths(1).AddDays(-1);

                    MeusAgendamentos = await AgendamentoService.GetByUserIdAsync(userId, start, end);
                }
            }
        }
    }
}