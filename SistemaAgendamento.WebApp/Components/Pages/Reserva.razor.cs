using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaAgendamento.Application.DTOs.Requests.Web;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Web;
using SistemaAgendamento.Application.Interfaces.Web;
using System.Security.Claims;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    public partial class Reserva : ComponentBase
    {
        [Inject]
        private IDisponibilidadeService _disponibilidadeService { get; set; } = null!;

        [Inject]
        private IAgendamentoService _agendamentoService { get; set; } = null!;

        [Inject]
        private ISolicitacaoService SolicitacaoService { get; set; } = null!;

        [Inject]
        private ISalaService _salaService { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        [Parameter]
        public int SalaId { get; set; }

        protected SalaResponse? SalaTarget { get; set; }
        protected int ClienteUsuarioId;

        protected DateTime SelectedDate { get; set; } = DateTime.Today;
        protected List<TimeSlotResponse> AvailableSlots { get; set; } = new();
        protected DateTime? SelectedSlot { get; set; }
        protected AgendamentoRequest agendamentoFinal { get; set; } = new AgendamentoRequest();
        protected string? mensagemStatus;
        private int SlotDurationMinutes => 30;
        protected bool showSolicitacaoModal { get; set; } = false;
        protected SolicitacaoRequest solicitacaoAtual { get; set; } = new();
        protected DateTime? horarioSolicitado { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            SalaTarget = await _salaService.GetByIdAsync(SalaId);

            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var userId))
            {
                ClienteUsuarioId = userId;
                agendamentoFinal.UsuarioId = ClienteUsuarioId;
            }

            await LoadAvailableSlots();
        }

        protected async Task LoadAvailableSlots()
        {
            if (SalaTarget == null) return;

            AvailableSlots = await _disponibilidadeService.GenerateTimeSlots(SalaId, SelectedDate);
            SelectedSlot = null;
        }

        protected void SelectSlot(DateTime slot)
        {
            SelectedSlot = slot;
            agendamentoFinal.DataHoraInicio = slot;
            agendamentoFinal.SalaId = SalaId;
        }

        protected async Task HandleBooking()
        {
            if (SelectedSlot == null)
            {
                mensagemStatus = "Por favor, selecione um horário.";
                return;
            }

            agendamentoFinal.UsuarioId = ClienteUsuarioId;
            agendamentoFinal.DataHoraFim = SelectedSlot.Value.AddMinutes(SlotDurationMinutes);

            try
            {
                if (string.IsNullOrWhiteSpace(agendamentoFinal.Titulo))
                {
                    mensagemStatus = "Por favor, preencha seu nome/email para o título.";
                    return;
                }

                await _agendamentoService.AddOrUpdateAsync(agendamentoFinal);

                mensagemStatus = $"Agendamento da sala {SalaTarget?.Nome} confirmado para {SelectedSlot.Value.ToString("dd/MM - HH:mm")}!";

                agendamentoFinal = new AgendamentoRequest { SalaId = SalaId, UsuarioId = ClienteUsuarioId };
                await LoadAvailableSlots();
            }
            catch (Exception ex)
            {
                mensagemStatus = $"Erro ao confirmar agendamento: {ex.Message}";
            }
        }
        protected void AbrirSolicitacao(int agendamentoId, DateTime horario)
        {
            solicitacaoAtual = new SolicitacaoRequest { AgendamentoId = agendamentoId };
            horarioSolicitado = horario;
            showSolicitacaoModal = true;
            mensagemStatus = null;
        }

        protected async Task EnviarSolicitacao()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(solicitacaoAtual.Justificativa))
                {
                    mensagemStatus = "A justificativa é obrigatória.";
                    return;
                }

                await SolicitacaoService.CriarSolicitacaoAsync(solicitacaoAtual, ClienteUsuarioId);

                mensagemStatus = "Solicitação enviada com sucesso! Aguarde a aprovação.";
                showSolicitacaoModal = false;
            }
            catch (Exception ex)
            {
                mensagemStatus = $"Erro: {ex.Message}";
            }
        }

        protected void FecharModal() => showSolicitacaoModal = false;
    }
}