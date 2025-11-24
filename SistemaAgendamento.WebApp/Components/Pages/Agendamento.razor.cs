using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authorization;
using SistemaAgendamento.Application.Interfaces.Web;
using SistemaAgendamento.Application.DTOs.Responses.Web;

namespace SistemaAgendamento.WebApp.Components.Pages
{
    [Authorize]
    public partial class Agendamento : ComponentBase
    {
        [Inject]
        private ISalaService SalaService { get; set; } = null!;

        [Inject]
        private NavigationManager NavManager { get; set; } = null!;

        public List<SalaResponse> ListaDeSalas { get; set; } = new();
        protected string? MensagemStatus;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ListaDeSalas = await SalaService.GetAllActiveAsync();
            }
            catch (Exception ex)
            {
                MensagemStatus = $"Erro ao carregar salas: {ex.Message}";
            }
        }

        protected void AbrirFormularioDeReserva(int salaId)
        {
            NavManager.NavigateTo($"/reserva/{salaId}");
        }
    }
}