using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Agendamento.WebApp.Services;
using Microsoft.AspNetCore.Components;
using Entities = Agendamento.Core.Entities;


namespace Agendamento.WebApp.Components.Pages
{
    public partial class Reserva : ComponentBase
    {
        [Inject]
        private IDisponibilidadeService DisponibilidadeService { get; set; } = null!;

        [Inject]
        private IAgendamentoService AgendamentoService { get; set; } = null!;

        [Inject]
        private AppDbContext DbContext { get; set; } = null!;

        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        [Parameter]
        public int TargetUserId { get; set; }

        private DateTime SelectedDate { get; set; } = DateTime.Today;
        private List<DateTime> AvailableSlots { get; set; } = new();
        private DateTime? SelectedSlot { get; set; }
        private Usuario? TargetUser { get; set; }

        private Entities.Agendamento agendamentoFinal = new Entities.Agendamento();
        private string? mensagemStatus;

        protected override async Task OnInitializedAsync()
        {
            TargetUser = await DbContext.Usuarios.FindAsync(TargetUserId);

            agendamentoFinal.UsuarioId = TargetUserId;

            await LoadAvailableSlots();
        }

        protected async Task LoadAvailableSlots()
        {
            AvailableSlots = await DisponibilidadeService.GenerateTimeSlots(TargetUserId, SelectedDate);
            SelectedSlot = null;
        }

        protected void SelectSlot(DateTime slot)
        {
            SelectedSlot = slot;
            agendamentoFinal.DataHoraInicio = slot;
        }

        protected async Task HandleBooking()
        {
            if (SelectedSlot == null)
            {
                mensagemStatus = "Por favor, selecione um horário.";
                return;
            }

            agendamentoFinal.DataHoraFim = SelectedSlot.Value.AddMinutes(30);

            try
            {
                await AgendamentoService.AddAsync(agendamentoFinal);

                mensagemStatus = "Agendamento confirmado com sucesso!";
                SelectedSlot = null;
                agendamentoFinal = new Entities.Agendamento { UsuarioId = TargetUserId };
                await LoadAvailableSlots();
            }
            catch (Exception ex)
            {
                mensagemStatus = $"Erro ao confirmar agendamento: {ex.Message}";
            }
        }

        protected async Task OnDateChange(ChangeEventArgs e)
        {
            if (DateTime.TryParse(e.Value?.ToString(), out var newDate))
            {
                SelectedDate = newDate;
                await LoadAvailableSlots();
            }
        }
    }
}