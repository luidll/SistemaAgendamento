using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.DesktopApp.Components.Pages
{
    public partial class Sistemas
    {
        [Inject]
        private AppDbContext DbContext { get; set; }

        private List<Sistema> listaSistemas;
        private Sistema sistemaAtual = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadSistemas();
        }

        private async Task LoadSistemas()
        {
            listaSistemas = await DbContext.Sistemas.ToListAsync();
        }

        private async Task Salvar()
        {
            if (sistemaAtual.Id == 0)
            {
                DbContext.Sistemas.Add(sistemaAtual);
            }
            else
            {
                DbContext.Sistemas.Update(sistemaAtual);
            }

            await DbContext.SaveChangesAsync();
            LimparFormulario();
            await LoadSistemas();
            StateHasChanged();
        }

        private void CarregarParaEdicao(Sistema sistema)
        {
            sistemaAtual = new Sistema
            {
                Id = sistema.Id,
                Nome = sistema.Nome,
                Ativo = sistema.Ativo
            };
        }

        private async Task Deletar(Sistema sistema)
        {
            DbContext.Sistemas.Remove(sistema);
            await DbContext.SaveChangesAsync();
            await LoadSistemas();
        }

        private void LimparFormulario()
        {
            sistemaAtual = new();
        }
    }
}