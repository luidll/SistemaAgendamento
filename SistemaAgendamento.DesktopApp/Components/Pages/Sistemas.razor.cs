using Microsoft.AspNetCore.Components;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Application.Interfaces.Desktop;

namespace SistemaAgendamento.DesktopApp.Components.Pages
{
    public partial class Sistemas
    {
        [Inject]
        private ISistemaService Service { get; set; } = null!;

        private List<SistemaResponse> listaSistemas = new();
        private SistemaRequest sistemaAtual = new();
        private int? sistemaEditandoId = null;

        protected override async Task OnInitializedAsync()
        {
            await LoadSistemas();
        }

        private async Task LoadSistemas()
        {
            listaSistemas = await Service.GetAllAsync();
        }

        private async Task Salvar()
        {
            await Service.AddOrUpdateAsync(sistemaAtual);
            LimparFormulario();
            await LoadSistemas();
        }

        private void CarregarParaEdicao(SistemaResponse sistema)
        {
            sistemaEditandoId = sistema.Id;
            sistemaAtual = new SistemaRequest
            {
                Nome = sistema.Nome,
                Ativo = sistema.Ativo
            };
        }

        private async Task Deletar(int id)
        {
            await Service.DeleteAsync(id);
            await LoadSistemas();
        }

        private void LimparFormulario()
        {
            sistemaEditandoId = null;
            sistemaAtual = new SistemaRequest();
        }
    }
}
