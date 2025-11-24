using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaAgendamento.Application.DTOs.Requests.Web;
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

        protected DateTime selectedDate { get; set; } = DateTime.Today;
        protected List<TimeSlotResponse> availableSlots { get; set; } = new();
        protected DateTime? SelectedSlot { get; set; }
        protected AgendamentoRequest agendamentoFinal { get; set; } = new AgendamentoRequest();
        protected string? mensagemStatus;
        protected bool showSolicitacaoModal { get; set; } = false;
        protected SolicitacaoRequest solicitacaoAtual { get; set; } = new();
        protected List<DateTime> validEndTimes { get; set; } = new();
        protected DateTime? selectedEndTime { get; set; }
        protected DateTime dataSolicitada { get; set; }
        protected string horaInicioSolicitada { get; set; } = "";
        protected string horaFimSolicitada { get; set; } = "";

        protected List<string> horariosInicio { get; set; } = new();
        protected List<string> horariosFim { get; set; } = new();

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

            availableSlots = await _disponibilidadeService.GenerateTimeSlots(SalaId, selectedDate);
            SelectedSlot = null;
        }

        protected void SelectSlot(DateTime startTime)
        {
            mensagemStatus = string.Empty;
            SelectedSlot = startTime;
            agendamentoFinal.DataHoraInicio = startTime;

            selectedEndTime = null;
            agendamentoFinal.DataHoraFim = DateTime.MinValue;
            validEndTimes.Clear();
            agendamentoFinal.SalaId = SalaId;
            var startIndex = availableSlots.FindIndex(s => s.Time == startTime);

            if (startIndex == -1) return;

            var currentCheck = startTime.AddMinutes(30);
            validEndTimes.Add(currentCheck);

            for (int i = startIndex + 1; i < availableSlots.Count; i++)
            {
                var slot = availableSlots[i];

                if (!slot.IsAvailable)
                    break;

                if (slot.Time == currentCheck)
                {
                    currentCheck = slot.Time.AddMinutes(30);
                    validEndTimes.Add(currentCheck);
                }
                else
                {
                    break;
                }
            }

            if (validEndTimes.Any())
            {
                selectedEndTime = validEndTimes.First();
                agendamentoFinal.DataHoraFim = selectedEndTime.Value;
            }
        }

        protected async Task HandleBooking()
        {
            if (SelectedSlot == null || selectedEndTime == null)
            {
                mensagemStatus = "Por favor, selecione o horário de início e fim.";
                return;
            }
            agendamentoFinal.UsuarioId = ClienteUsuarioId;
            agendamentoFinal.DataHoraFim = selectedEndTime.Value;

            try
            {
                if (string.IsNullOrWhiteSpace(agendamentoFinal.Titulo))
                {
                    mensagemStatus = "Por favor, preencha seu nome/email para o título.";
                    return;
                }

                await _agendamentoService.AddOrUpdateAsync(agendamentoFinal);

                mensagemStatus = $"Agendamento confirmado: {SelectedSlot.Value:HH:mm} até {selectedEndTime.Value:HH:mm}!";

                agendamentoFinal = new AgendamentoRequest { SalaId = SalaId, UsuarioId = ClienteUsuarioId };
                validEndTimes.Clear();
                SelectedSlot = null;
                selectedEndTime = null;
                await LoadAvailableSlots();
            }
            catch (Exception ex)
            {
                mensagemStatus = $"Erro ao confirmar agendamento: {ex.Message}";
            }
        }
        protected async Task AbrirSolicitacaoAsync(int agendamentoId, DateTime horario)
        {
            dataSolicitada = horario;
            solicitacaoAtual = new SolicitacaoRequest { AgendamentoId = agendamentoId, DataHoraInicioSolicitada = horario };
            var agendamento = await _agendamentoService.GetByIdAsync(agendamentoId);
            if(agendamento != null)
            {
                GerarHorarios(solicitacaoAtual.DataHoraInicioSolicitada, agendamento.DataHoraFim);
            }
            horaInicioSolicitada = horario.ToString("HH:mm");
            AtualizarHorariosFim();
            showSolicitacaoModal = true;
            mensagemStatus = null;

        }

        protected async Task EnviarSolicitacao()
        {
            try
            {
                if (string.IsNullOrEmpty(horaFimSolicitada))
                {
                    mensagemStatus = "Horário fim é obrigatório.";
                    return;
                }
                    
                var fimTime = TimeSpan.Parse(horaFimSolicitada);


                solicitacaoAtual.DataHoraFimSolicitada = dataSolicitada.Date + fimTime;

                if (string.IsNullOrWhiteSpace(solicitacaoAtual.Justificativa))
                {
                    mensagemStatus = "A justificativa é obrigatória.";
                    showSolicitacaoModal = false;
                    return;
                }
                if (solicitacaoAtual.DataHoraInicioSolicitada >= solicitacaoAtual.DataHoraFimSolicitada)
                {
                    mensagemStatus = "Horário de início deve ser anterior ao horário de término.";
                    showSolicitacaoModal = false;
                    return;
                }

                await SolicitacaoService.CriarSolicitacaoAsync(solicitacaoAtual, ClienteUsuarioId);

                mensagemStatus = "Solicitação enviada com sucesso! Aguarde a aprovação.";
                showSolicitacaoModal = false;
            }
            catch (Exception ex)
            {
                mensagemStatus = $"Erro: {ex.Message}";
                showSolicitacaoModal = false;
                return;
            }
        }

        private string FormatarDuracao(TimeSpan duration)
        {
            if (duration.TotalMinutes < 60)
                return $"{duration.TotalMinutes} min";

            var horas = (int)duration.TotalHours;
            var minutos = duration.Minutes;

            var stringHoras = horas == 1 ? "hora" : "horas";

            if (minutos > 0)
                return $"{horas} {stringHoras} e {minutos} min";

            return $"{horas} {stringHoras}";
        }
        protected void GerarHorarios(DateTime dataInicio, DateTime dataFim)
        {
            horariosInicio.Clear();
            horariosFim.Clear();

            var atual = dataInicio;

            while (atual <= dataFim)
            {
                horariosInicio.Add(atual.ToString("HH:mm"));
                atual = atual.AddMinutes(30);
            }
        }
        private void OnInicioChanged(ChangeEventArgs e)
        {
            horaInicioSolicitada = e.Value?.ToString() ?? "";

            AtualizarHorariosFim();

            horaFimSolicitada = horariosFim.FirstOrDefault() ?? "";
        }


        protected void AtualizarHorariosFim()
        {
            if (string.IsNullOrWhiteSpace(horaInicioSolicitada))
                return;

            var inicio = TimeSpan.Parse(horaInicioSolicitada);

            horariosFim = horariosInicio
                .Select(h => TimeSpan.Parse(h))
                .Where(t => t > inicio)
                .Select(t => t.ToString(@"hh\:mm"))
                .ToList();
        }




        protected void FecharModal() => showSolicitacaoModal = false;
    }
}