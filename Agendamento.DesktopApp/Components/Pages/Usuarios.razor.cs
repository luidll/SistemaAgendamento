using Agendamento.Core.Entities;
using Agendamento.Infrastructure.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Agendamento.DesktopApp.Components.Pages
{
    public partial class Usuarios
    {
        [Inject]
        private AppDbContext DbContext { get; set; } = null!;

        private List<Usuario> listaUsuarios = new();
        private Usuario usuarioAtual = new();

        private string senha = string.Empty;
        private string confirmarSenha = string.Empty;
        private string? mensagemErro;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsuarios();
        }

        private async Task LoadUsuarios()
        {
            listaUsuarios = await DbContext.Usuarios.AsNoTracking().ToListAsync();
        }

        private async Task Salvar()
        {
            mensagemErro = null;

            if (string.IsNullOrWhiteSpace(usuarioAtual.Login) || string.IsNullOrWhiteSpace(usuarioAtual.NomeCompleto))
            {
                mensagemErro = "Login e Nome são obrigatórios.";
                return;
            }

            if (usuarioAtual.Id == 0)
            {
                if (string.IsNullOrWhiteSpace(senha) || senha != confirmarSenha)
                {
                    mensagemErro = "Senhas não conferem ou estão em branco.";
                    return;
                }

                usuarioAtual.SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(senha))
                {
                    if (senha != confirmarSenha)
                    {
                        mensagemErro = "Senhas não conferem.";
                        return;
                    }
                    usuarioAtual.SenhaHash = BCrypt.Net.BCrypt.HashPassword(senha);
                }
            }

            if (usuarioAtual.Id == 0)
            {
                DbContext.Usuarios.Add(usuarioAtual);
            }
            else
            {
                DbContext.Usuarios.Update(usuarioAtual);
            }

            await DbContext.SaveChangesAsync();
            LimparFormulario();
            await LoadUsuarios();
            StateHasChanged();
        }

        private void CarregarParaEdicao(Usuario usuario)
        {
            usuarioAtual = new Usuario
            {
                Id = usuario.Id,
                NomeCompleto = usuario.NomeCompleto,
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Telefone = usuario.Telefone,
                Login = usuario.Login,
                SenhaHash = usuario.SenhaHash,
                Setor = usuario.Setor,
                Ramal = usuario.Ramal
            };
        }

        private async Task Deletar(Usuario usuario)
        {
            DbContext.Usuarios.Remove(usuario);
            await DbContext.SaveChangesAsync();
            await LoadUsuarios();
        }

        private void LimparFormulario()
        {
            usuarioAtual = new();
            senha = "";
            confirmarSenha = "";
            mensagemErro = null;
        }

        private async Task ResetarSenha(Usuario usuario)
        {
            var senhaReset = "123456";
            usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(senhaReset);

            DbContext.Usuarios.Update(usuario);
            await DbContext.SaveChangesAsync();
        }
    }
}