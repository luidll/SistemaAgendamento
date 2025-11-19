using Microsoft.AspNetCore.Components;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;
using SistemaAgendamento.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAgendamento.DesktopApp.Components.Pages
{
    public partial class Permissoes
    {
        [Inject] private IUsuarioService UsuarioService { get; set; } = null!;
        [Inject] private IRotinaService RotinaService { get; set; } = null!;

        private List<UsuarioResponse> listaUsuarios = new();
        private List<RotinaResponse> todasAsRotinas = new();
        private UsuarioResponse? usuarioAtual;

        private List<RotinaResponse> listaRotinasDisponiveis = new();
        private List<RotinaResponse> listaRotinasConcedidas = new();

        private int[] rotinasDisponiveisSelecionadas = Array.Empty<int>();
        private int[] rotinasConcedidasSelecionadas = Array.Empty<int>();

        protected override async Task OnInitializedAsync()
        {
            listaUsuarios = await UsuarioService.ListarAsync();
            todasAsRotinas = await RotinaService.ListarAtivasAsync();
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

            usuarioAtual = await UsuarioService.ObterComPermissoesAsync(usuarioId);
            AtualizarListasRotinas();
        }

        private void AtualizarListasRotinas()
        {
            if (usuarioAtual == null)
            {
                listaRotinasConcedidas = new();
                listaRotinasDisponiveis = new();
                return;
            }

            listaRotinasConcedidas = usuarioAtual.Rotinas?.ToList() ?? new List<RotinaResponse>();

            listaRotinasDisponiveis = todasAsRotinas
                .Where(r => !listaRotinasConcedidas.Any(c => c.Id == r.Id))
                .ToList();
        }

        private async Task Conceder()
        {
            if (usuarioAtual == null || rotinasDisponiveisSelecionadas?.Length == 0) return;

            await UsuarioService.ConcederRotinasAsync(usuarioAtual.Id, rotinasDisponiveisSelecionadas);

            usuarioAtual = await UsuarioService.ObterComPermissoesAsync(usuarioAtual.Id);
            AtualizarListasRotinas();

            rotinasDisponiveisSelecionadas = Array.Empty<int>();
            StateHasChanged();
        }

        private async Task Revogar()
        {
            if (usuarioAtual == null || rotinasConcedidasSelecionadas?.Length == 0) return;

            await UsuarioService.RevogarRotinasAsync(usuarioAtual.Id, rotinasConcedidasSelecionadas);

            usuarioAtual = await UsuarioService.ObterComPermissoesAsync(usuarioAtual.Id);
            AtualizarListasRotinas();

            rotinasConcedidasSelecionadas = Array.Empty<int>();
            StateHasChanged();
        }
    }
}
