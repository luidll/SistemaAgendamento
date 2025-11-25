using Microsoft.AspNetCore.Components;
using SistemaAgendamento.Application.DTOs.Requests.Desktop;
using SistemaAgendamento.Application.DTOs.Responses.Desktop;
using SistemaAgendamento.Application.Interfaces.Desktop;

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
        private void MascaraCpf(ChangeEventArgs e)
        {
            var valor = ApenasNumeros(e.Value?.ToString());

            if (valor.Length > 11)
                valor = valor.Substring(0, 11);

            if (valor.Length > 9)
                valor = valor.Insert(3, ".").Insert(7, ".").Insert(11, "-");
            else if (valor.Length > 6)
                valor = valor.Insert(3, ".").Insert(7, ".");
            else if (valor.Length > 3)
                valor = valor.Insert(3, ".");

            usuarioAtual.Cpf = valor;
        }
        private void MascaraTelefone(ChangeEventArgs e)
        {
            var valor = ApenasNumeros(e.Value?.ToString());

            if (valor.Length > 11)
                valor = valor.Substring(0, 11);

            if (valor.Length == 0)
            {
                usuarioAtual.Telefone = "";
                return;
            }

            if (valor.Length > 2)
                valor = valor.Insert(0, "(").Insert(3, ") ");
            else
                valor = valor.Insert(0, "(");

            if (valor.Length > 10)
                valor = valor.Insert(10, "-");
            else if (valor.Length > 6)
            {
                if (valor.Length >= 10)
                    valor = valor.Insert(9, "-");
            }

            usuarioAtual.Telefone = valor;
        }
        private string ApenasNumeros(string str)
        {
            if (string.IsNullOrEmpty(str)) return "";
            return new string(str.Where(char.IsDigit).ToArray());
        }
    }
}
