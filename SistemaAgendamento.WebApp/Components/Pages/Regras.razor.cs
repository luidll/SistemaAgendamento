using SistemaAgendamento.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using SistemaAgendamento.Application.Interfaces;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    [Authorize]
    public partial class Regras : ComponentBase
    {
        [Inject]
        private IDisponibilidadeService DisponibilidadeService { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        private RegraDisponibilidade regraAtual { get; set; } = new RegraDisponibilidade();
        private List<RegraDisponibilidade> listaRegras = new();
        private int _currentUserId;
        private string? mensagemErro;

        private string HoraInicioString { get; set; } = "09:00:00";
        private string HoraFimString { get; set; } = "17:00:00";

        protected IEnumerable<DayOfWeek> DaysOfWeek { get; set; } = Enum.GetValues<DayOfWeek>();

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
                    await LoadRegras();
                }
            }
        }

        private async Task LoadRegras()
        {
            listaRegras = await DisponibilidadeService.GetRegrasByUserIdAsync(_currentUserId);
        }

        protected async Task SalvarRegra()
        {
            mensagemErro = null;

            if (!TimeSpan.TryParse(HoraInicioString, out var horaInicio) ||
                !TimeSpan.TryParse(HoraFimString, out var horaFim))
            {
                mensagemErro = "Formato de hora inválido. Use HH:mm ou HH:mm:ss.";
                return;
            }

            if (regraAtual.DuracaoSlotMinutos <= 0)
            {
                mensagemErro = "A duração do slot deve ser maior que zero.";
                return;
            }

            regraAtual.UsuarioId = _currentUserId;
            regraAtual.HoraInicio = horaInicio;
            regraAtual.HoraFim = horaFim;

            try
            {
                await DisponibilidadeService.AddOrUpdateRegraAsync(regraAtual);

                LimparFormulario();
                await LoadRegras();
            }
            catch (Exception ex)
            {
                mensagemErro = $"Erro ao salvar: {ex.Message}";
            }
        }

        protected async Task DeletarRegra(int id)
        {
            await DisponibilidadeService.DeleteRegraAsync(id);
            await LoadRegras();
        }

        protected void CarregarParaEdicao(RegraDisponibilidade regra)
        {
            regraAtual = regra;
            HoraInicioString = regra.HoraInicio.ToString(@"hh\:mm\:ss");
            HoraFimString = regra.HoraFim.ToString(@"hh\:mm\:ss");
        }

        protected void LimparFormulario()
        {
            regraAtual = new RegraDisponibilidade();
            regraAtual.DiaDaSemana = DayOfWeek.Monday;
            HoraInicioString = "09:00:00";
            HoraFimString = "17:00:00";
            mensagemErro = null;
        }
    }
}