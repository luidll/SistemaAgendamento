using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.DesktopApp.Components.Pages
{
    public partial class Modulos
    {
        [Inject]
        private AppDbContext DbContext { get; set; } = null!;

        private List<Modulo> listaModulos = new();
        private List<Sistema> listaSistemas = new();
        private Modulo moduloAtual = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadSistemas();
            await LoadModulos();
        }

        private async Task LoadModulos()
        {
            listaModulos = await DbContext.Modulos
                                          .Include(m => m.Sistema)
                                          .AsNoTracking()
                                          .ToListAsync();
        }

        private async Task LoadSistemas()
        {
            listaSistemas = await DbContext.Sistemas
                                           .Where(s => s.Ativo)
                                           .ToListAsync();
        }

        private async Task Salvar()
        {
            if (moduloAtual.SistemaId == 0)
                return;

            if (moduloAtual.Id == 0)
            {
                DbContext.Modulos.Add(moduloAtual);
            }
            else
            {
                DbContext.Modulos.Update(moduloAtual);
            }

            await DbContext.SaveChangesAsync();
            LimparFormulario();
            await LoadModulos();
            StateHasChanged();
        }

        private void CarregarParaEdicao(Modulo modulo)
        {
            moduloAtual = new Modulo
            {
                Id = modulo.Id,
                Nome = modulo.Nome,
                Ativo = modulo.Ativo,
                SistemaId = modulo.SistemaId,
                Url = modulo.Url,
                Icon = modulo.Icon
            };
        }

        private async Task Deletar(Modulo modulo)
        {
            DbContext.Modulos.Remove(modulo);
            await DbContext.SaveChangesAsync();
            await LoadModulos();
        }

        private void LimparFormulario()
        {
            moduloAtual = new();
        }
    }
}
