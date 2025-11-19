using Microsoft.AspNetCore.Components;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;

namespace SistemaAgendamento.DesktopApp.Components.Pages
{
    public partial class Modulos
    {
        [Inject] private IModuloService ModuloService { get; set; } = null!;

        private List<ModuloResponse> listaModulos = new();
        private ModuloRequest moduloAtual = new();

        protected override async Task OnInitializedAsync()
        {
            listaModulos = await ModuloService.GetAllAsync();
        }

        private async Task Salvar()
        {
            if (moduloAtual.SistemaId == 0)
                return;

            await ModuloService.AddOrUpdateAsync(moduloAtual);

            moduloAtual = new();
            listaModulos = await ModuloService.GetAllAsync();
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
            listaModulos = await ModuloService.GetAllAsync();
        }

        private void LimparFormulario()
        {
            moduloAtual = new();
        }
    }
}
