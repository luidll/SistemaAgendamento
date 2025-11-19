using SistemaAgendamento.Domain.Entities;
using SistemaAgendamento.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAgendamento.DesktopApp.Components.Pages
{
    public partial class Rotinas
    {
        [Inject]
        private AppDbContext DbContext { get; set; } = null!;

        private List<Rotina> listaRotinas = new List<Rotina>();
        private List<Modulo> listaModulos = new List<Modulo>();
        private Rotina rotinaAtual = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadModulos();
            await LoadRotinas();
        }
        private async Task LoadRotinas()
        {
            listaRotinas = await DbContext.Rotinas
                                          .Include(r => r.Modulo)
                                            .ThenInclude(m => m.Sistema)
                                          .AsNoTracking()
                                          .ToListAsync();
        }

        private async Task LoadModulos()
        {
            listaModulos = await DbContext.Modulos
                                          .Where(m => m.Ativo)
                                          .Include(m => m.Sistema)
                                          .ToListAsync();
        }

        private async Task Salvar()
        {
            if (rotinaAtual.ModuloId == 0)
            {
                return;
            }

            if (rotinaAtual.Id == 0)
            {
                DbContext.Rotinas.Add(rotinaAtual);
            }
            else
            {
                DbContext.Rotinas.Update(rotinaAtual);
            }

            await DbContext.SaveChangesAsync();
            LimparFormulario();
            await LoadRotinas();
            StateHasChanged();
        }

        private void CarregarParaEdicao(Rotina rotina)
        {
            rotinaAtual = new Rotina
            {
                Id = rotina.Id,
                Nome = rotina.Nome,
                Ativo = rotina.Ativo,
                ModuloId = rotina.ModuloId
            };
        }

        private async Task Deletar(Rotina rotina)
        {
            DbContext.Rotinas.Remove(rotina);
            await DbContext.SaveChangesAsync();
            await LoadRotinas();
        }

        private void LimparFormulario()
        {
            rotinaAtual = new();
        }
    }
}