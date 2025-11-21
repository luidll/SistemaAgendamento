using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Application.Interfaces.Desktop;
using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAgendamento.DesktopApp.Components.Pages
{
    public partial class Rotinas
    {
        [Inject]
        private IModuloService ModuloService { get; set; } = null!;
        [Inject]
        private IRotinaService RotinaService { get; set; } = null!;

        private List<RotinaResponse> listaRotinas = new List<RotinaResponse>();
        private List<ModuloResponse> listaModulos = new List<ModuloResponse>();
        private RotinaRequest rotinaAtual = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadModulos();
            await LoadRotinas();
        }
        private async Task LoadRotinas()
        {
            listaRotinas = await RotinaService.GetAllAsync();
        }

        private async Task LoadModulos()
        {
            listaModulos = await ModuloService.GetAllAsync();
        }

        private async Task Salvar()
        {
            if (rotinaAtual.ModuloId == 0)
            {
                return;
            }

            await RotinaService.AddOrUpdateAsync(rotinaAtual);

            LimparFormulario();
            await LoadRotinas();
            StateHasChanged();
        }

        private void CarregarParaEdicao(RotinaResponse rotina)
        {
            rotinaAtual = new RotinaRequest
            {
                Id = rotina.Id,
                Nome = rotina.Nome,
                Ativo = rotina.Ativo,
                ModuloId = rotina.ModuloId
            };
        }

        private async Task Deletar(int id)
        {
            await RotinaService.DeletarAsync(id);
            await LoadRotinas();
        }

        private void LimparFormulario()
        {
            rotinaAtual = new();
        }
    }
}