using Microsoft.AspNetCore.Components;
using SistemaAgendamento.Application.DTOs.Requests;
using SistemaAgendamento.Application.DTOs.Responses;
using SistemaAgendamento.Application.Interfaces;

namespace SistemaAgendamento.DesktopApp.Components.Pages
{
    public partial class Usuarios
    {
        [Inject]
        private IUsuarioService UsuarioService { get; set; } = null!;

        private List<UsuarioResponse> listaUsuarios = new();
        private UsuarioRequest usuarioAtual = new();

        private string senha = string.Empty;
        private string confirmarSenha = string.Empty;
        private string? mensagemErro;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsuarios();
        }

        private async Task LoadUsuarios()
        {
            listaUsuarios = await UsuarioService.ListarAsync();
        }

        private async Task Salvar()
        {
            mensagemErro = null;

            usuarioAtual.Senha = senha;
            usuarioAtual.ConfirmarSenha = confirmarSenha;

            var erro = await UsuarioService.SalvarAsync(usuarioAtual);

            if (erro != null)
            {
                mensagemErro = erro;
                return;
            }

            await LoadUsuarios();
            LimparFormulario();
            StateHasChanged();
        }

        private async Task CarregarParaEdicao(UsuarioResponse usuario)
        {
            var req = await UsuarioService.ObterParaEdicaoAsync(usuario.Id);
            if (req != null)
            {
                usuarioAtual = req;
            }

            senha = "";
            confirmarSenha = "";
        }

        private async Task Deletar(UsuarioResponse usuario)
        {
            await UsuarioService.ExcluirAsync(usuario.Id);
            await LoadUsuarios();
        }

        private void LimparFormulario()
        {
            usuarioAtual = new();
            senha = "";
            confirmarSenha = "";
            mensagemErro = null;
        }

        private async Task ResetarSenha(UsuarioResponse usuario)
        {
            var req = new UsuarioRequest
            {
                Id = usuario.Id,
                Senha = "123456",
                ConfirmarSenha = "123456"
            };

            await UsuarioService.SalvarAsync(req);
            await LoadUsuarios();
        }
    }
}
