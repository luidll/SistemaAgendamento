using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agendamento.DesktopApp.Components.Pages
{
    public partial class Permissoes
    {
        [Inject]
        private AppDbContext DbContext { get; set; } = null!;

        private List<Usuario> listaUsuarios = new();
        private List<Rotina> todasAsRotinas = new();
        private Usuario? usuarioAtual;

        private List<Rotina> listaRotinasDisponiveis = new();
        private List<Rotina> listaRotinasConcedidas = new();

        private int[] rotinasDisponiveisSelecionadas = Array.Empty<int>();
        private int[] rotinasConcedidasSelecionadas = Array.Empty<int>();

        protected override async Task OnInitializedAsync()
        {
            listaUsuarios = await DbContext.Usuarios.OrderBy(u => u.NomeCompleto).ToListAsync();
            todasAsRotinas = await DbContext.Rotinas.Where(r => r.Ativo)
                                                  .OrderBy(r => r.Nome ?? string.Empty)
                                                  .ToListAsync();
        }
        private async Task OnUsuarioSelecionado(ChangeEventArgs e)
        {
            var usuarioId = Convert.ToInt32(e.Value);
            if (usuarioId == 0)
            {
                usuarioAtual = null;
                listaRotinasDisponiveis.Clear();
                listaRotinasConcedidas.Clear();
                return;
            }

            usuarioAtual = await DbContext.Usuarios
                                .Include(u => u.RotinasPermitidas)
                                .FirstOrDefaultAsync(u => u.Id == usuarioId);

            AtualizarListasRotinas();
        }

        private void AtualizarListasRotinas()
        {
            if (usuarioAtual == null) return;

            listaRotinasConcedidas = usuarioAtual.RotinasPermitidas.ToList();

            listaRotinasDisponiveis = todasAsRotinas
                .ExceptBy(listaRotinasConcedidas.Select(r => r.Id), r => r.Id)
                .ToList();
        }

        private async Task Conceder()
        {
            if (usuarioAtual == null || rotinasDisponiveisSelecionadas == null) return;

            var rotinasParaAdicionar = todasAsRotinas
                .Where(r => rotinasDisponiveisSelecionadas.Contains(r.Id))
                .ToList();

            foreach (var rotina in rotinasParaAdicionar)
            {
                usuarioAtual.RotinasPermitidas.Add(rotina);
            }

            await DbContext.SaveChangesAsync();
            AtualizarListasRotinas();
            StateHasChanged();
        }

        // Botão "<<"
        private async Task Revogar()
        {
            if (usuarioAtual == null || rotinasConcedidasSelecionadas == null) return;

            var rotinasParaRemover = usuarioAtual.RotinasPermitidas
                .Where(r => rotinasConcedidasSelecionadas.Contains(r.Id))
                .ToList();

            foreach (var rotina in rotinasParaRemover)
            {
                usuarioAtual.RotinasPermitidas.Remove(rotina);
            }

            await DbContext.SaveChangesAsync();
            AtualizarListasRotinas();
            StateHasChanged();
        }
    }
}