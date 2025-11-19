using Microsoft.AspNetCore.Components;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Services;

namespace SistemaAgendamento.DesktopApp.Components.Pages
{
    public partial class Sistemas
    {
        [Inject]
        private SistemaService Service { get; set; } = null!;

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
            if (sistemaEditandoId == null)
            {
                await Service.AddAsync(sistemaAtual);
            }
            else
            {
                await Service.UpdateAsync(sistemaEditandoId.Value, sistemaAtual);
            }

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
