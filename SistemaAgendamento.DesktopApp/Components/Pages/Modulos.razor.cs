using Microsoft.AspNetCore.Components;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Application.Services;

namespace SistemaAgendamento.DesktopApp.Components.Pages
{
    public partial class Modulos
    {
        [Inject] private IModuloService ModuloService { get; set; } = null!;
        [Inject] private ISistemaService SistemaService { get; set; } = null!;

        private List<ModuloResponse> listaModulos = new();
        private List<SistemaResponse> listaSistemas = new();
        private ModuloRequest moduloAtual = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadModulos();
            listaSistemas = await SistemaService.GetAllAsync();
        }
        private async Task LoadModulos()
        {
            listaModulos = await ModuloService.GetAllAsync();
        }
        private async Task Salvar()
        {
            if (moduloAtual.SistemaId == 0)
                return;

            await ModuloService.AddOrUpdateAsync(moduloAtual);

            moduloAtual = new();
            LimparFormulario();
            await LoadModulos();
            StateHasChanged();
        }

        private void CarregarParaEdicao(ModuloResponse modulo)
        {
            moduloAtual = new ModuloRequest
            {
                Id = modulo.Id,
                Nome = modulo.Nome,
                Ativo = modulo.Ativo,
                SistemaId = modulo.SistemaId,
                Url = modulo.Url,
                Icon = modulo.Icon
            };
        }

        private async Task Deletar(int id)
        {
            await ModuloService.DeleteAsync(id);
            await LoadModulos();
        }

        private void LimparFormulario()
        {
            moduloAtual = new();
        }
    }
}
