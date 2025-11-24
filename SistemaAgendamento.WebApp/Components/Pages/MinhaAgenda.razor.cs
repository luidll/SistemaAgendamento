using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using System.Security.Claims;
using MudBlazor;
using SistemaAgendamento.Application.DTOs.Requests.Web;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    public partial class MinhaAgenda : ComponentBase
    {
        [Inject] private IAgendamentoService AgendamentoService { get; set; } = null!;
        [Inject] private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        protected List<AgendamentoResponse> MeusAgendamentos { get; set; } = new();
        protected string UserName { get; set; } = "Usuário";
        protected DateTime DataAtual { get; set; } = DateTime.Today;
        protected List<DateTime> DiasDoCalendario { get; set; } = new();
        protected string PeriodoTexto => DataAtual.ToString("MMMM yyyy", new System.Globalization.CultureInfo("pt-BR")).ToUpper();
        [Parameter] public AgendamentoResponse Agendamento { get; set; } = null!;
        protected MudDialog _modalDialog;
        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            UserName = user.FindFirstValue(ClaimTypes.GivenName) ?? user.FindFirstValue(ClaimTypes.Name) ?? "Usuário";

            if (user.Identity?.IsAuthenticated == true)
            {
                await CarregarDadosDoMes();
            }
        }

        protected async Task CarregarDadosDoMes()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var userIdString = authState.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (int.TryParse(userIdString, out var userId))
            {
                var start = new DateTime(DataAtual.Year, DataAtual.Month, 1).AddDays(-7);
                var end = start.AddMonths(2);

                MeusAgendamentos = await AgendamentoService.GetByUserIdAsync(userId, start, end);
                GerarGradeCalendario();
            }
        }

        private void GerarGradeCalendario()
        {
            DiasDoCalendario.Clear();
            var primeiroDiaMes = new DateTime(DataAtual.Year, DataAtual.Month, 1);
            int diasOffset = (int)primeiroDiaMes.DayOfWeek;
            var dataInicioGrid = primeiroDiaMes.AddDays(-diasOffset);

            for (int i = 0; i < 42; i++)
            {
                DiasDoCalendario.Add(dataInicioGrid.AddDays(i));
            }
        }

        protected async Task AlterarMes(int meses)
        {
            DataAtual = DataAtual.AddMonths(meses);
            await CarregarDadosDoMes();
        }

        protected async Task IrParaHoje()
        {
            DataAtual = DateTime.Today;
            await CarregarDadosDoMes();
        }

        protected void AbrirDetalhes(AgendamentoResponse agendamento)
        {
            Agendamento = new AgendamentoResponse
            {
                Id = agendamento.Id,
                Titulo = agendamento.Titulo,
                SalaNome = agendamento.SalaNome,
                ResponsavelNome = agendamento.ResponsavelNome,
                DataHoraInicio = agendamento.DataHoraInicio,
                DataHoraFim = agendamento.DataHoraFim,
                Descricao = agendamento.Descricao,
                UsuarioId = agendamento.UsuarioId,
                SalaId = agendamento.SalaId
            };

            _modalDialog.ShowAsync();
            StateHasChanged();
        }

        protected void FecharDetalhes()
        {
            _modalDialog.CloseAsync();
        }

        private async Task Editar()
        {
            if (Agendamento == null) return;

            var agendamentoRequest = new AgendamentoRequest
            {
                Id = Agendamento.Id,
                Titulo = Agendamento.Titulo,
                Descricao = Agendamento.Descricao,
                DataHoraInicio = Agendamento.DataHoraInicio,
                DataHoraFim = Agendamento.DataHoraFim,
                UsuarioId = Agendamento.UsuarioId,
                SalaId = Agendamento.SalaId
            };

            await AgendamentoService.AddOrUpdateAsync(agendamentoRequest);

            Snackbar.Add("Agendamento atualizado com sucesso!", Severity.Success);
            FecharDetalhes();
            await CarregarDadosDoMes();
        }

        private async Task Excluir()
        {
            if (Agendamento == null) return;

            await AgendamentoService.DeleteAsync(Agendamento.Id);

            Snackbar.Add("Agendamento excluído", Severity.Error);
            FecharDetalhes();
            await CarregarDadosDoMes();
        }
        protected string GetCorSala(string sala)
        {
            if (sala.ToLower().Contains("reunião")) return Colors.Teal.Default;
            if (sala.ToLower().Contains("auditório")) return Colors.DeepOrange.Default;
            return Colors.Blue.Default;
        }
    }
}