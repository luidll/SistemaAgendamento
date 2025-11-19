using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    public partial class Reserva : ComponentBase
    {
        [Inject]
        private IDisponibilidadeService DisponibilidadeService { get; set; } = null!;

        [Inject]
        private IAgendamentoService AgendamentoService { get; set; } = null!;

        [Inject]
        private ISalaService SalaService { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider AuthStateProvider { get; set; } = null!;

        [Parameter]
        public int SalaId { get; set; }

        protected SalaResponse? SalaTarget { get; set; }
        protected int ClienteUsuarioId;

        protected DateTime SelectedDate { get; set; } = DateTime.Today;
        protected List<DateTime> AvailableSlots { get; set; } = new();
        protected DateTime? SelectedSlot { get; set; }
        protected Agendamento agendamentoFinal { get; set; } = new Agendamento();
        protected string? mensagemStatus;
        private int SlotDurationMinutes => 30;

        protected override async Task OnParametersSetAsync()
        {
            SalaTarget = await SalaService.GetByIdAsync(SalaId);

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

            AvailableSlots = await DisponibilidadeService.GenerateTimeSlots(SalaId, SelectedDate);
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

                await AgendamentoService.AddAsync(agendamentoFinal);

                mensagemStatus = $"Agendamento da sala {SalaTarget?.Nome} confirmado para {SelectedSlot.Value.ToString("dd/MM - HH:mm")}!";

                agendamentoFinal = new Agendamento { SalaId = SalaId, UsuarioId = ClienteUsuarioId };
                await LoadAvailableSlots();
            }
            catch (Exception ex)
            {
                mensagemStatus = $"Erro ao confirmar agendamento: {ex.Message}";
            }
        }
    }
}